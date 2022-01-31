using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class AggregatedSearchResult
    {
        public AggregatedSearchResult()
        {
            ImdbResults = new();
            YoutubeResults = new();
        }
        public string? SearchedTerm {get; set;}
        public List<ImdbSearchResultItem> ImdbResults { get; set; }
        public List<YoutubeSearchResultItem> YoutubeResults { get; set; }
       

    }
}
