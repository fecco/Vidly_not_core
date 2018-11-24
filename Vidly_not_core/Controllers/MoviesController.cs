using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//az Include() metódus az alábbi namespace-en belül van:
using System.Data.Entity;
using System.Web.Mvc;
using Vidly_not_core.Models;
using Vidly_not_core.ViewModels;

namespace Vidly_not_core.Controllers
{
    public class MoviesController : Controller
    {
        //DBContext kell az adatbázishoz való kapcsolódás miatt
        private ApplicationDbContext _context;
        //ctor + [tab] és beadja az alábbi inicializálót:
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        //a _context disposable, aminek a proper használata:
        protected override void Dispose(bool disposing)
        {
            //az alapbeállítást kicseréljük a saját contextünkre
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        }

        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}
        
        //az alábbi sor használható az attribútum routing engedélyezése után:
        //a regex() és range() fv-ekkel constraints eket adhatunk a kifejezéshez
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        //Mosh megoldása a 2-es feladatra:
        //public ViewResult Index()
        //{
        //    var movies = GetMovies();

        //    return View(movies);
        //}

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.MovieGenre).ToList();

            return View(movies);
        }
        public ActionResult Details(int id)
        {
            //az alábbi is azonnal végrehajtótuk a plusz hozzáadott methodhívás miatt
            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

    }
}