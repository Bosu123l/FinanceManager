using FinanceManager.Services;
using System;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    public partial class FinancialBalanceController : Controller
    {
        private readonly IncomeService _incomeService;
        private readonly OutGoingService _outGoingService;

        public FinancialBalanceController(IncomeService incomeService, OutGoingService outGoingService)
        {
            _incomeService = incomeService;
            _outGoingService = outGoingService;
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
                return Json(_incomeService.SumOfIncoming(), JsonRequestBehavior.AllowGet);
            }

            return Json(_incomeService.SumOfIncoming(firstdateTime.Value, seconddateTime.Value), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomesByTimeFilter/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public virtual ActionResult GetIncomesByTimeFilter(DateTime? firstDateTime, DateTime? secondDateTime)
        {
            var tempDate = DateTime.Now;

            return Json(_incomeService.GetIncomes(firstDateTime.GetValueOrDefault(new DateTime(tempDate.Year, tempDate.Month, 1)), new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month))), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsByNumberOfDays(int? days)
        {
            return Json(_incomeService.GetIncomesByNumberOfDays(days.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomesByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetIncomesByNumberOfWeeks(int? weeks)
        {
            return Json(_incomeService.GetIncomesByNumberOfDays(weeks.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetIncomingsByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetIncomingsByNumberOfMonth(int? month)
        {
            return Json(_incomeService.GetIncomeByNumberOfMonth(month.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetIncomes")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncomes()
        {
            return Json(_incomeService.GetIncomes(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncome/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncome(long id)
        {
            return Json(_incomeService.GetIncome(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetIncomesByLastOperations/{count}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetIncomesByLastOperations(int? count)
        {
            return Json(_incomeService.GetIncomesByLastOperations(count.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        #endregion Incomings
        
        #region Outgoings

        [Route("/SumOfOutgoings/{firstdateTime?}/{seconddateTime?}")]
        [HttpGet]
        public virtual ActionResult SumOfOutgoings(DateTime? firstdateTime, DateTime? seconddateTime)
        {
            if (firstdateTime == null || seconddateTime == null)
            {
                return Json(_outGoingService.SumOfOutgoings(), JsonRequestBehavior.AllowGet);
            }

            return Json(_outGoingService.SumOfOutgoings(firstdateTime.Value, seconddateTime.Value), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByTimeFilter/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByTimeFilter(DateTime? firstDateTime, DateTime? secondDateTime)
        {
            var tempDate = DateTime.Now;
            return Json(_outGoingService.GetOutGoings(firstDateTime.GetValueOrDefault(new DateTime(tempDate.Year, tempDate.Month, 1)), new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month))), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfDays/{days}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfDays(int? days)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfDays(days.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfWeeks/{weeks}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfWeeks(int? weeks)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfWeeks(weeks.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetOutgoingsByNumberOfMonth/{month}")]
        [HttpGet]
        public virtual ActionResult GetOutgoingsByNumberOfMonth(int? month)
        {
            return Json(_outGoingService.GetOutgoingsByNumberOfMonth(month.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("/GetOutGoings")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoings()
        {
            return Json(_outGoingService.GetOutGoings(), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoing/{id}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoing(long id)
        {
            return Json(_outGoingService.GetOutGoing(id), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.Route("GetOutgoingsByLastOperations/{count}")]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult GetOutgoingsByLastOperations(int? count)
        {
            return Json(_outGoingService.GetOutgoingsByLastOperations(count.GetValueOrDefault(0)), JsonRequestBehavior.AllowGet);
        }

        #endregion Outgoings
    }
}