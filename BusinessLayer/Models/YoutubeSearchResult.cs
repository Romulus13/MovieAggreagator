namespace BusinessLayer.Model
{
    public class YoutubeSearchResult
    {

        public YoutubeSearchResult()
        {
            Videos = new List<string>();
            Channels = new List<string>();
            Playlists = new List<string>();
        }

        public List<string>? Videos      {get; set;}
        public List<string>? Channels { get; set; }
        public List<string>? Playlists { get; set; }


    }
}
