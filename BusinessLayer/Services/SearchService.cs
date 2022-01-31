using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using BusinessLayer.Models;
using DataLayer.Interfaces;
using Google.Apis.YouTube.v3.Data;
using IMDbApiLib.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using static Google.Apis.YouTube.v3.SearchResource;

namespace MovieAggreagator.Youtube
{
    public class SearchService : ISearchService
    {
        private IMapper _autoMapper;
        private ISearchFactory _searchClientFactory;
        private IMemoryCache _memoryCache;
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        private IImdbDataService _imdbDataService;
        private IYoutubeDataService _youtubeDataService;

        public SearchService(ISearchFactory searchClientFactory, IMapper autoMapper, 
                             IMemoryCache memoryCache, ILogger<SearchService> logger, 
                             IUnitOfWork unitOfWork, IImdbDataService imdbDataService, IYoutubeDataService youtubeDataService)
        {
            _searchClientFactory = searchClientFactory;
            _autoMapper = autoMapper;
            _memoryCache = memoryCache;
            _logger = logger;
            _unitOfWork = unitOfWork;
             _youtubeDataService = youtubeDataService;
            _imdbDataService = imdbDataService;
        }


        public async Task<IEnumerable<YoutubeSearchResultItem>> GetFromYoutube(string searchQuery)
        {

            var results = new List<YoutubeSearchResultItem>();
            if (_searchClientFactory == null || _searchClientFactory.YouTubeService == null)
            {
                return results;
            }
            ListRequest searchListRequest = _searchClientFactory.YouTubeService.Search.List("snippet");
            searchListRequest.Q = searchQuery;
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();
            foreach (var item in searchListResponse.Items)
            {
                var result = new YoutubeSearchResultItem();
                result.MapSearchResult(item);
                results.Add(result);
            }

            return results;
        }

        public async Task<ImdbSearchResult> GetFromImdb(string searchQuery)
        {
            var result = new ImdbSearchResult();
            if (_searchClientFactory == null || _searchClientFactory.ImdbService == null)
            {
                return result;
            }
            SearchData data = await _searchClientFactory.ImdbService.SearchAsync(searchQuery);
            result = _autoMapper.Map<ImdbSearchResult>(data);
            return result;
        }

        public async Task<AggregatedSearchResult> GetAggreagatedSearchResult(string searchQuery)
        {
            AggregatedSearchResult result = new AggregatedSearchResult();
            if (String.IsNullOrWhiteSpace(searchQuery))
            {
                return result;
            }

            result.SearchedTerm = searchQuery;
            //try to get it from cache
            if (_memoryCache.TryGetValue(searchQuery, out AggregatedSearchResult cacheResult) && cacheResult != null)
            {
                _logger.LogDebug($"Took it from the cache {searchQuery}");
                return cacheResult;
            }

            //if there is no such search query in the cache try to
            //fetch the aggreagated data from the DB in order to
            //avoid using quota on youtube and imdb
            //try to use fuzzy string search here, replace all white spaces in the middle of the string
            var imdbStoredResults = await _unitOfWork.ImdbResults.Find(ir =>ir.Title != null && ir.Title.Trim().ToUpper().Contains(searchQuery.Trim().ToUpper()));

            if (imdbStoredResults is null || !imdbStoredResults.Any())
            {
                var imdbData = await GetFromImdb(searchQuery);
                
                result.ImdbResults = imdbData.Results ?? new();
                await _imdbDataService.SaveAll(result.ImdbResults);
            }
            else
            {
                result.ImdbResults = imdbStoredResults.Select(str => _autoMapper.Map<ImdbSearchResultItem>(str)).ToList();
            }

            var youtubeStoredResults = await _unitOfWork.YoutubeResults.Find(ir => ir.Title != null && ir.Title.Trim().ToUpper().Contains(searchQuery.Trim().ToUpper()));
            if (youtubeStoredResults is null || !youtubeStoredResults.Any())
            {
                var searchListResponse = await GetFromYoutube(searchQuery);
                result.YoutubeResults = searchListResponse.ToList();
                await _youtubeDataService.SaveAll(result.YoutubeResults);
            }
            else
            {
                result.YoutubeResults = youtubeStoredResults.Select(str => _autoMapper.Map<YoutubeSearchResultItem>(str)).ToList();
            }

            //move memorycahce options to a configuration file
            MemoryCacheEntryOptions cacheOption = new()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddDays(1) - DateTime.Now)
            };

            _memoryCache.Set(searchQuery, result, cacheOption);


            return result;
        }
    }
}
