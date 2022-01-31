using BusinessLayer.Model;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IYoutubeDataService
    {   
        
        ///Due to time constraint I would implement this later
        public Task<YoutubeSearchResultItem> Get(int id);

      public Task<List<YoutubeSearchResultItem>> GetAll();

        public Task<YoutubeSearchResultItem> Create(YoutubeSearchResultItem resItem);
        
        public Task<YoutubeSearchResultItem> Update(YoutubeSearchResultItem resItem);

        public Task Delete(int id);

        public Task SaveAll(IEnumerable<YoutubeSearchResultItem> youtubeResult);

    }
}
