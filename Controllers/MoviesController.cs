using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesDotNetCore.Model;
using MoviesDotNetCore.Repositories;

namespace MoviesDotNetCore.Controllers
{
    [ApiController]
    [Route("/")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [Route("/movie/{title}")]
        [HttpGet]
        public async Task<Movie> GetMovieDetails([FromRoute(Name = "title")] string title)
        {
            return await _movieRepository.FindByTitle(title);
        }

        [Route("/movie/{title}/vote")]
        [HttpPost]
        public async Task<int> VoteInMovie([FromRoute(Name = "title")] string title)
        {
            return await _movieRepository.VoteByTitle(title);
        }

        [Route("/movie/{title}/{updatedTitle}/updateTitle")]
        [HttpPost]
        public async Task<int> VoteInMovie([FromRoute(Name = "title")] string title, [FromRoute(Name = "updatedTitle")] string updatedTitle)
        {
            return await _movieRepository.UpdateTitle(title, updatedTitle);
        }

        [Route("/search")]
        [HttpGet]
        public async Task<List<Movie>> SearchMovies([FromQuery(Name = "q")] string search)
        {
            return await _movieRepository.Search(search);
        }

        [Route("/graph")]
        [HttpGet]
        public async Task<D3Graph> FetchD3Graph([FromQuery(Name = "limit")] int limit)
        {
            return await _movieRepository.FetchD3Graph(limit <= 0 ? 50 : limit);
        }

        [Route("/movie/{title}/delete")]
        [HttpPost]
        public async Task<int> DeleteMovie([FromRoute(Name = "title")] string title)
        {
            return await _movieRepository.DeleteByTitle(title);
        }
    }
}
