using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyStore.Models;
using VidlyStore.ViewModels;

namespace VidlyStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer

        private VidlyContext _context;

        public CustomerController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

      
        public ActionResult Customer()
        {
            var viewModel = new CustomerViewModel();
            viewModel.customers = _context.customers.Include(c => c.MemberShipType).ToList();
            return View(viewModel);

           /// return View();
        }

      
        [Route("Customer/New")]
        public ActionResult New()
        {
            var membershipTypes = _context.memberShipTypes.ToList();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MemberShipType = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MemberShipType = _context.memberShipTypes.ToList()

                };
                return View("CustomerForm", viewModel);
            }
           
            if(customer.Id == 0)
                _context.customers.Add(customer);
            else
            {
                var c = _context.customers.Single(m => m.Id == customer.Id);
                c.Name = customer.Name;
                if(customer.DOB == null)
                {
                    c.DOB = null;
                }
                else
                {
                    c.DOB = Convert.ToDateTime(customer.DOB.ToString());
                }
                
                c.MemberShipTypeId = customer.MemberShipTypeId;
                c.IsSubscribeToNewsLetter = customer.IsSubscribeToNewsLetter;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                Console.WriteLine(ex);
            }
               
            return RedirectToAction("Customer", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var cus = _context.customers.SingleOrDefault(c => c.Id == id);
            if (cus == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = cus,
                MemberShipType = _context.memberShipTypes.ToList()
            };
            
            return View("CustomerForm", viewModel);
        }

        //public IEnumerable<Customer> GetCustomer()
        //{
        //    return new List<Customer>()
        //    {
        //        new Customer  { Id = 1,Name = "Kashmira" },
        //        new Customer  { Id = 2,Name = "Tushar" },
        //        new Customer  { Id = 3,Name = "Kruti" },
        //        new Customer  { Id= 4,Name = "Manan" },
        //    };
        //}

        //public ViewResult Index()
        //{
        //    var cus = _context.customers;
        //    return View(cus);
        //}

        //[Route("customers/details/{id}")]
        //public ActionResult Details(int id)
        //{
        //    var cus = _context.customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);
        //    if (cus == null)
        //        return HttpNotFound();
        //    return View(cus);
        //    //return Content(cus.Name);

        //}

    }
}