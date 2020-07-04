using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.Controllers
{
    

    
    public class NewRentalController : ApiController
    {
        private VidlyContext _context;

        public NewRentalController()
        {
            _context = new VidlyContext();
        }

        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            if (newRentalDto.MovieIDs.Count == 0)
                return BadRequest("No movie id has been given.");

            var Customer = _context.customers.
                Where(c => c.Id == newRentalDto.CustomerID).
                Single();
            if (Customer == null)
                return BadRequest("Invalid CustomerID");

            var Movies = _context.movie
                .Where(m => newRentalDto.MovieIDs.Contains(m.Id)).ToList();

            if (Movies.Count != newRentalDto.MovieIDs.Count)
                return BadRequest("One or more MovieId is Invalid.");

            foreach(var movie in Movies)
            {
                if (movie.NoofAvailableStock == 0)
                    return BadRequest("Movie is not available");

                movie.NoofAvailableStock--;
                var rental = new Rental
                {
                    Customer = Customer,
                    Movie = movie,
                    DateRated = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            //using ok because here we can not create single object it create one to many objects so return ok
            return Ok();

            


            throw new NotImplementedException();
        }
    }
}
