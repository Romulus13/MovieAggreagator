using Google.Apis.YouTube.v3;
using IMDbApiLib;

namespace BusinessLayer.Interfaces
{
    public interface ISearchFactory
    {
        YouTubeService? YouTubeService { get; set; }

        ApiLib? ImdbService { get; set; }
    }
}
