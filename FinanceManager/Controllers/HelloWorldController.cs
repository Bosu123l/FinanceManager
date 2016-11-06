using Domain;
using Domain.Models;
using System;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class HelloWorldController : Controller
    {
        private Repository<Income> _inComeRepository;

        public HelloWorldController(Repository<Income> inComeRepository)
        {
            if (inComeRepository == null)
            {
                throw new ArgumentException(nameof(inComeRepository));
            }
            _inComeRepository = inComeRepository;
            int i = 0;
        }

        // GET: HelloWorld
        public ActionResult Index()
        {
            return View("FirstView");
        }

        // GET: HelloWorld/Welcome
        public virtual ActionResult Welcome()
        {
            return View("FirstView");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}