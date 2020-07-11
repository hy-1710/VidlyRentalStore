using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VidlyStore.Models;
using VidlyStore.ViewModels;

namespace VidlyStore.Controllers
{
    public class MovieController : Controller
    {
        private VidlyContext _context;

        public MovieController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movie
        public ActionResult Random()
        {
            
            var viewModel = new RandomMovieViewModel()
            {
                movie = _context.movie.Include(m=> m.Genre).ToList()
               
            };
            if(User.IsInRole(RoleName.CanManageMovies))
            { return View("Random",viewModel); }
            else { return View("ReadOnlyList",viewModel); }
            
        }

     
        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genre = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                
                
                Genre = genre
            };
            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                   

                    Genre = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                Movie m = new Movie();
                m = movie;
                m.CreatedOn = DateTime.Now;
                _context.movie.Add(m);
            }
                
            else
            {
                var mv = _context.movie.Single(m => m.Id == movie.Id);
                mv.Name = movie.Name;
                mv.ReleasedDate = Convert.ToDateTime(movie.ReleasedDate.ToString());
                mv.GenreId = movie.GenreId;
                mv.NoinStock = movie.NoinStock;

            }
            _context.SaveChanges();

            return RedirectToAction("Random", "Movie");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.movie.Single(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                
                Genre = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);

        }


        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));


        //}

        //public ActionResult Details(int id)
        //{
        //    var movie = _context.movie.Include(m => m.genre).SingleOrDefault(m => m.Id == id);
        //    if (movie == null)
        //        return HttpNotFound();
        //    return View(movie);
        //}
    }
}