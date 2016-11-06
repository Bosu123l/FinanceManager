using Domain;
using System;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class HomeController : Controller
    {
        private IPersonRepository _personRepository;

        public HomeController(IPersonRepository personRepository)
        {
            if (personRepository == null)
            {
                throw new ArgumentException(nameof(personRepository));
            }
            _personRepository = personRepository;
            int i = 0;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}