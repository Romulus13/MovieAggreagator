using BusinessLayer.Model;

namespace BusinessLayer.Interfaces
{
    public interface IImdbDataService
    {   
        /*Due to time constraint I would implement this later*/
        public Task<ImdbSearchResultItem> Get(int id);

      public Task<List<ImdbSearchResultItem>> GetAll();

        public Task<ImdbSearchResultItem> Create(ImdbSearchResultItem resItem);
        public Task<ImdbSearchResultItem> Update(ImdbSearchResultItem resItem);

        public Task Delete(int id);
        ///Due to time constraint I would implement this later
      
        public Task SaveAll(IEnumerable<ImdbSearchResultItem> imdbResults);
    }
}
