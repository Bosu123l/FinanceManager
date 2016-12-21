using Domain.Models;
using FinanceManager.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    [System.Web.Mvc.Route("api /[controller]")]
    public partial class FinancialManagerController : Controller
    {
        private readonly IncomeService _incomeService;
        private readonly OutGoingService _outGoingService;
        private readonly TypeOfOutgoingService _typeOfOutgoingService;
        private readonly SourceOfAmountService _sourceOfAmountService;

        public FinancialManagerController(IncomeService incomeService, OutGoingService outGoingService, TypeOfOutgoingService typeOfOutgoingService, SourceOfAmountService sourceOfAmountService)
        {
            _incomeService = incomeService;
            _outGoingService = outGoingService;
            _typeOfOutgoingService = typeOfOutgoingService;
            _sourceOfAmountService = sourceOfAmountService;
        }

        // GET: FinancialManager
        public virtual ActionResult Index()
        {
            return View("MainView");
        }

        public virtual ActionResult AddView()
        {
            return View("AddView");
        }

        public virtual ActionResult Edit()
        {
            return View("EditView");
        }

        [System.Web.Mvc.Route("/GetIncomes")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncomes()
        {
            return Json(_incomeService.GetIncomes(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetOutGoings")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoings()
        {
            return Json(_outGoingService.GetOutGoings(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncome/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncome(long id)
        {
            return Json(_incomeService.GetIncome(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoing/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoing(long id)
        {
            return Json(_outGoingService.GetOutGoing(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("DeleteIncome/{id}")]
        [System.Web.Mvc.HttpDelete]
        public virtual ActionResult DeleteIncome(long id)
        {
            return Json(_incomeService.RemoveIncome(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("DeleteOutgoing/{id}")]
        [System.Web.Mvc.HttpDelete]
        public virtual ActionResult DeleteOutgoing(long id)
        {
            return Json(_outGoingService.RemoveOutGoing(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddIncome/")]
        [System.Web.Mvc.HttpPost]
        public virtual ActionResult AddIncome([FromBody]Income income)
        {
            return Json(_incomeService.AddIncome(income), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddOutgoing/")]
        [System.Web.Mvc.HttpPost]
        public virtual ActionResult AddOutgoing([FromBody]Outgoing outgoing)
        {
            return Json(_outGoingService.AddOutgoing(outgoing), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("UpdateIncome/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult UpdateIncome([FromBody]Income income)
        {
            return Json(_incomeService.UpdateIncome(income), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("UpdateOutgoing/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult UpdateOutgoing([FromBody]Outgoing outgoing)
        {
            return Json(_outGoingService.UpdateOutgoing(outgoing), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddSourceOfAmount/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult AddSourceOfAmount([FromBody] SourceOfAmount sourceOfAmount)
        {
            return Json(_sourceOfAmountService.AddSourceOfAmount(sourceOfAmount), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddTypeOfOutgoing/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult AddTypeOfOutgoing([FromBody] TypeOfOutgoing typeOfOutgoing)
        {
            return Json(_typeOfOutgoingService.AddTypeOfOutgoing(typeOfOutgoing));
        }
    }
}