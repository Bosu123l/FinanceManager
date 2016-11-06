using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult GetPersons()
        {
            List<Person> persons = new List<Person>()
            {
                new Person()
                {
                    Name="Andrzej",
                    SubName="Wajda",
                    Age=12
                },
                new Person()
                {
                    Name="Andrze2j",
                    SubName="Wajda31",
                    Age=1232
                }
            };
            return Json(persons, JsonRequestBehavior.AllowGet);
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