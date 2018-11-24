using System;
using System.Collections.Generic;
//az Include() metódus az alábbi namespace-en belül van:
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly_not_core.Models;

namespace Vidly_not_core.Controllers
{
    public class CustomersController : Controller
    {
        //DBContext kell az adatbázishoz való kapcsolódás miatt
        private ApplicationDbContext _context;
        //ctor + [tab] és beadja az alábbi inicializálót:
        public CustomersController()
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
        public ViewResult Index()
        {
            //az eredeti függvényhívás helyett áttérünk az adatbázisos verzióra:
            //var customers = GetCustomers();
            //innentől a customers property egy DBset ami DBcontext-ként van definiálva
            //amivel minden adatbázisban szereplő customert elérhetünk.
            //amikor ez a sor végrehajtódik, az Entity framework nem csinálja meg a query-t az adatbázisban
            //akkor történik meg a futtatás. amikor iteráljuk a változót (pl foreach-el)
            //ezt hívják deffered execution-nek 
            //a .ToList() hívással azonnal is végrehajthatjuk ha akarjuk
            //az Iclude ahhoz kell, hogy plusz kapcsolódó objektumokat is betölthessünk a customer mellé.
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            //az alábbi is azonnal végrehajtótuk a plusz hozzáadott methodhívás miatt
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        //miven fent áttértünk az adatbázishoz, így innen eltűntethetjük az alábbi függvényt:
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}
    }
}