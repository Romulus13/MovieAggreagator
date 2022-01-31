using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using IMDbApiLib;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Clients
{
    /// <summary>

    /// 
    /// </summary>
    public class SearchClientFactory : ISearchFactory
    {
        private readonly ILogger<SearchClientFactory> _logger;
        private readonly IConfiguration _configuration;
        private YouTubeService? _youTubeService;
        private ApiLib? _imdbService;
        public SearchClientFactory(ILogger<SearchClientFactory> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            Setup();
        }

        private void Setup()
        {
            try
            {
                Run();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    _logger.LogDebug("Error: " + e.Message);
                }
            }
            _logger.LogDebug("Creating the service completed");
        }

        public YouTubeService? YouTubeService { get => _youTubeService; set => _youTubeService = value; }

        public ApiLib? ImdbService { get => _imdbService; set => _imdbService = value; }
        private void Run()
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _configuration.GetSection("YoutubeApiKey").Value,
                ApplicationName = GetType().ToString()
            });
            _logger.LogDebug("Got youtube service");
           
            _imdbService = new ApiLib(_configuration.GetSection("ImdbApiKey").Value);
            _logger.LogDebug("Got imdb service");
        }
    }
}
