using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.Controllers.Api
{
    [Authorize]
    public class MoviesController : ApiController
    {

        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        //Get /api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.movie
        //.Include(m => m.Genre)
                .Where(m => m.NoofAvailableStock > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }


        //GET /api/movies/1


        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.movie.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(movie));


        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //here source is moviedto and destination movie
            //because we save the data from moviedto to movie
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.movie.Add(movie);
            _context.SaveChanges();

            //movie returns id so set it to moviedto and return in URI
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _context.movie.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            Mapper.Map(movieDto, movie);

            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]

        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.movie.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _context.movie.Remove(movie);
            _context.SaveChanges();
            return Ok();

        }
    }
}
 