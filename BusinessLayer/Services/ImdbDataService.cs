using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class ImdbDataService : IImdbDataService
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ImdbDataService(IMapper mapper, ILogger<ImdbDataService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<ImdbSearchResultItem> Create(ImdbSearchResultItem resItem)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ImdbSearchResultItem> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImdbSearchResultItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAll(IEnumerable<ImdbSearchResultItem> imdbResults)
        {
            if (imdbResults == null || !imdbResults.Any())
            {
                return;
            }
            var imdbStoredResults = imdbResults.Select(imr => _mapper.Map<ImdbStoredResult>(imr));
            await _unitOfWork.ImdbResults.AddRange(imdbStoredResults);
            _unitOfWork.SaveChanges();
        }

        public Task<ImdbSearchResultItem> Update(ImdbSearchResultItem resItem)
        {
            throw new NotImplementedException();
        }
    }
}
