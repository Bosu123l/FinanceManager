using FinanceManager.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public partial class FinancialBalanceController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly IOutGoingService _outGoingService;
        private readonly ITypeOfOutgoingService _typeOfOutgoingService;
        private readonly ISourceOfAmountService _sourceOfAmountService;
        private readonly string _idLoggedUser;

        public FinancialBalanceController(IIncomeService incomeService, IOutGoingService outGoingService,
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
            catch (Exception ex)
            {
                _idLoggedUser = string.Empty;
            }
        }

        public virtual ActionResult Index()
        {
            return View("ManView");
        }

        #region Incomings

        [Route("/SumOfIncomings/{firstdateTime?}/{seconddateTime?}")]
        [HttpGet]
        public virtual ActionResult SumOfIncomings(DateTime? firstdateTime, DateTime? seconddateTime)
        {
            if (firstdateTime == null || seconddateTime == null)
            {
                return Json(_incomeService.SumOfIncoming(_idLoggedUser), JsonRequestBehavior.AllowGet);
            }

         
            return Json(_incomeService.SumOfIncoming(firstdateTime.Value, seconddateTime.Value, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomesByTimeFilter/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public virtual ActionResult GetIncomesByTimeFilter(DateTime? firstDateTime, DateTime? secondDateTime)
        {
            var tempDate = DateTime.Now;

            return Json(_incomeService.GetIncomes(firstDateTime.GetValueOrDefault(new DateTime(tempDate.Year, tempDate.Month, 1)), new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month)), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsByNumberOfDays(int? days)
        {
            return Json(_incomeService.GetIncomesByNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsSumByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsSumByNumberOfDays(int? days)
        {
            return Json(_incomeService.SumOfIncomingByNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomesByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetIncomesByNumberOfWeeks(int? weeks)
        {
            return Json(_incomeService.GetIncomesByNumberOfDays(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsSumByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsSumByNumberOfWeeks(int? weeks)
        {
            return Json(_incomeService.SumOfIncomingByNumberOfWeeks(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsByNumberOfMonth(int? month)
        {
            return Json(_incomeService.GetIncomeByNumberOfMonth(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsSumByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsSumByNumberOfMonth(int? month)
        {
            return Json(_incomeService.SumOfIncomingByNumberOfMonth(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetIncomes")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncomes()
        {
            return Json(_incomeService.GetIncomes(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncome/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncome(long id)
        {
            return Json(_incomeService.GetIncome(id, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncomesByLastOperations/{count}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncomesByLastOperations(int? count)
        {
            return Json(_incomeService.GetIncomesByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsSumByLastOperations/{count}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsSumByLastOperations(int? count)
        {
            return Json(_incomeService.SumOfIncomingByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetSourceOfAmounts")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetSourceOfAmounts()
        {
            return Json(_sourceOfAmountService.GetSourceOfAmounts(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        #endregion Incomings

        #region Outgoings

        [Route("/SumOfOutgoings/{firstdateTime?}/{seconddateTime?}")]
        [HttpGet]
        public virtual ActionResult SumOfOutgoings(DateTime? firstdateTime, DateTime? seconddateTime)
        {
            if (firstdateTime == null || seconddateTime == null)
            {
                return Json(_outGoingService.SumOfOutgoings(_idLoggedUser), JsonRequestBehavior.AllowGet);
            }

            return Json(_outGoingService.SumOfOutgoings(firstdateTime.Value, seconddateTime.Value, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByTimeFilter/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByTimeFilter(DateTime? firstDateTime, DateTime? secondDateTime)
        {
            var tempDate = DateTime.Now;
            return Json(_outGoingService.GetOutGoings(firstDateTime.GetValueOrDefault(new DateTime(tempDate.Year, tempDate.Month, 1)), new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month)), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsSumByNumberOfMonth/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsSumByTimeFilter(DateTime? firstDateTime, DateTime? secondDateTime)
        {
            var tempDate = DateTime.Now;
            return Json(_outGoingService.SumOfOutgoings(firstDateTime.GetValueOrDefault(new DateTime(tempDate.Year, tempDate.Month, 1)), new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month)), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfDays(int? days)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsSumByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsSumByNumberOfDays(int? days)
        {
            return Json(_outGoingService.SumOfOutgoingsByNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfWeeks(int? weeks)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfWeeks(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsSumByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsSumByNumberOfWeeks(int? weeks)
        {
            return Json(_outGoingService.SumOfOutgoingsByNumberOfWeeks(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfMonth(int? month)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfMonth(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsSumByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsSumByNumberOfMonth(int? month)
        {
            return Json(_outGoingService.SumOfOutgoingsByNumberOfWeeks(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetOutGoings")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoings()
        {
            return Json(_outGoingService.GetOutGoings(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoing/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoing(long id)
        {
            return Json(_outGoingService.GetOutGoing(id, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoingsByLastOperations/{count}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoingsByLastOperations(int? count)
        {
            return Json(_outGoingService.GetOutgoingsByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoingsSumByLastOperations/{count}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoingsSumByLastOperations(int? count)
        {
            return Json(_outGoingService.SumOfOutgoingsByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetTypeOfOutgoings")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetTypeOfOutgoings()
        {
            return Json(_typeOfOutgoingService.GetTypeOfOutgoings(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }
      
        #endregion Outgoings
    }
}