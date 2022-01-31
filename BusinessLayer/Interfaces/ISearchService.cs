using BusinessLayer.Model;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<YoutubeSearchResultItem>> GetFromYoutube(string searchQuery);
        Task<ImdbSearchResult> GetFromImdb(string searchQuery);

        Task<AggregatedSearchResult> GetAggreagatedSearchResult(string searchQuery);
    }
}