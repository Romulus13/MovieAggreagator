using BusinessLayer.Enums;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class YoutubeSearchResultItem
    {
        public string? Title { get; set; }
        public YoutubeResultTypeEnum Type { get; set; }

        public string? ResourceId { get; set; }

        public string? Description { get; set; }

        public void MapSearchResult(SearchResult resItem)
        {
            if (resItem is null)
            {
                return;
            }
            switch (resItem.Id.Kind)
            {
                case "youtube#video":
                    this.Type = YoutubeResultTypeEnum.Video;
                    this.ResourceId = resItem.Id.VideoId;
                    break;

                case "youtube#channel":
                    this.Type = YoutubeResultTypeEnum.Channel;
                    this.ResourceId = resItem.Id.ChannelId;
                    
                    break;

                case "youtube#playlist":
                    this.Type = YoutubeResultTypeEnum.Playlist;
                    this.ResourceId = resItem.Id.PlaylistId;
                    break;
                    default:
                        return;
            }
            this.Title = resItem.Snippet.Title;
            this.Description = resItem.Snippet.Description;
        }
    }
}
