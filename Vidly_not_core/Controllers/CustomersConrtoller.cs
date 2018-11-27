using System;
using System.Collections.Generic;
//az Include() metódus az alábbi namespace-en belül van:
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly_not_core.Models;
using Vidly_not_core.ViewModels;

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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFomrViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            //a korábban create action-t save-re cseréltük, hogy ne kelljen külön implementálni egy update-et, hanem ezt
            //használhassuk a létrehozásra és a módosításra egyaránt.
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                //először a DB-ből lekérjük az Id alapján a már meglévő customert.
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //ez a proper way ahogyan updatelni illik egy adatbázis onjektumot a Microsoft szerint:

                //TryUpdateModel(customerInDb);

                //Mosh szerint ugyanakkor security issuet is jelent ez az approach mivel ilyenkor a teljes tartalmat updateli 
                //nem csak azokat a mezőket, amikben valóban változás van.

                //ehelyett azonban mindig jobb, ha megadjuk a pontos listát, hogy mit szeretnénk update-elni a DB-ben:
                customerInDb.Name = customer.Name;
                customerInDb.CustomersBirthDate = customer.CustomersBirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedtoNewsLetter = customer.IsSubscribedtoNewsLetter;


            }
            //azzal, hogy a _contexthez adtuk a customert, az még nem kerülk a DB-be, csak a memóriába
            //a _context folyamatosan trackeli a változásokat amiken átessett, de csak akkor véglegesíti, ha mentjük azokat
            
            //miután áttértünk a save action-re már nem kell az Add!
            //_context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFomrViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
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