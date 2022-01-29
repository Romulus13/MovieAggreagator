using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieAggreagator.Youtube;

namespace MovieAggreagator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
       
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }
        

        [HttpGet("~/[controller]/youtube/{searchString}")]
        public async Task<IActionResult> GetFromYoutube(string searchString)
        {
            var item = await _searchService.GetFromYoutube(searchString);

            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("~/[controller]/imdb/{searchString}")]
        public async Task<IActionResult> GetFromImdb(string searchString)
        {
            var item = await _searchService.GetFromImdb(searchString);

            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("~/[controller]/aggregated/{searchString}")]
        public async Task<IActionResult> GetAggregatedResults(string searchString)
        {
            var item = await _searchService.GetAggreagatedSearchResult(searchString);

            if (item == null)
                return NotFound();
            return Ok(item);
        }

    }
}