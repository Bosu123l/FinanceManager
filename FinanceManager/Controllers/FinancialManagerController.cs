using FinanceManager.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Income = FinanceManager.Entities.Income;
using Outgoing = FinanceManager.Entities.Outgoing;
using SourceOfAmount = FinanceManager.Entities.SourceOfAmount;
using TypeOfOutgoing = FinanceManager.Entities.TypeOfOutgoing;

namespace FinanceManager.Controllers
{
    [System.Web.Mvc.Authorize]
    [System.Web.Mvc.Route("api /[controller]")]
    public partial class FinancialManagerController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly IOutGoingService _outGoingService;
        private readonly ITypeOfOutgoingService _typeOfOutgoingService;
        private readonly ISourceOfAmountService _sourceOfAmountService;
        private readonly string _idLoggedUser;

        public FinancialManagerController(IIncomeService incomeService, IOutGoingService outGoingService,
            ITypeOfOutgoingService typeOfOutgoingService, ISourceOfAmountService sourceOfAmountService)
        {
            _incomeService = incomeService;
            _outGoingService = outGoingService;
            _typeOfOutgoingService = typeOfOutgoingService;
            _sourceOfAmountService = sourceOfAmountService;

            try
            {
                _idLoggedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id;
            }
            catch (Exception)
            {
                _idLoggedUser = string.Empty;
            }
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
            return Json(_incomeService.GetIncomes(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetOutGoings")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoings()
        {
            return Json(_outGoingService.GetOutGoings(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncome/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncome(long id)
        {
            return Json(_incomeService.GetIncome(id, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoing/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoing(long id)
        {
            return Json(_outGoingService.GetOutGoing(id, _idLoggedUser), JsonRequestBehavior.AllowGet);
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
            return Json(_outGoingService.RemoveOutGoing(id, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddIncome/")]
        [System.Web.Mvc.HttpPost]
        public virtual ActionResult AddIncome([FromBody]Income income)
        {
            var tempIncome = income;
            tempIncome.UserId = _idLoggedUser;

            return Json(_incomeService.AddIncome(tempIncome), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddOutgoing/")]
        [System.Web.Mvc.HttpPost]
        public virtual ActionResult AddOutgoing([FromBody]Outgoing outgoing)
        {
            var tempOutgoing = outgoing;
            tempOutgoing.UserId = _idLoggedUser;

            return Json(_outGoingService.AddOutgoing(tempOutgoing), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("UpdateIncome/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult UpdateIncome([FromBody]Income income)
        {
            var tempIncome = income;
            tempIncome.UserId = _idLoggedUser;

            return Json(_incomeService.UpdateIncome(tempIncome), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("UpdateOutgoing/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult UpdateOutgoing([FromBody]Outgoing outgoing)
        {
            var tempOutgoing = outgoing;
            tempOutgoing.UserId = _idLoggedUser;

            return Json(_outGoingService.UpdateOutgoing(tempOutgoing), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddSourceOfAmount/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult AddSourceOfAmount([FromBody] SourceOfAmount sourceOfAmount)
        {
            var tempSourceOfAmount = sourceOfAmount;
            tempSourceOfAmount.UserId = _idLoggedUser;

            return Json(_sourceOfAmountService.AddSourceOfAmount(tempSourceOfAmount), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("AddTypeOfOutgoing/")]
        [System.Web.Mvc.HttpPut]
        public virtual ActionResult AddTypeOfOutgoing([FromBody] TypeOfOutgoing typeOfOutgoing)
        {
            var tempTypeOfOutgoing = typeOfOutgoing;
            tempTypeOfOutgoing.UserId = _idLoggedUser;

            return Json(_typeOfOutgoingService.AddTypeOfOutgoing(tempTypeOfOutgoing));
        }
    }
}