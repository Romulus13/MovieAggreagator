using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using BusinessLayer.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class YoutubeDataService : IYoutubeDataService
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;


        public YoutubeDataService(IMapper mapper, ILogger<YoutubeDataService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public Task<YoutubeSearchResultItem> Create(YoutubeSearchResultItem resItem)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ///Due to time constraint I would implement this later
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ///Due to time constraint I would implement this later
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<YoutubeSearchResultItem> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<YoutubeSearchResultItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAll(IEnumerable<YoutubeSearchResultItem> youtubeResult)
        {
            if (youtubeResult == null || !youtubeResult.Any())
            {
                return; 
            }
            var youtubeStoredResults= youtubeResult.Select(yr => _mapper.Map<YoutubeSearchResultItem, YoutubeStoredResult>(yr));
            await _unitOfWork.YoutubeResults.AddRange(youtubeStoredResults);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// ///Due to time constraint I would implement this later
        /// </summary>
        /// <param name="resItem"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<YoutubeSearchResultItem> Update(YoutubeSearchResultItem resItem)
        {
            throw new NotImplementedException();
        }
    }
}
