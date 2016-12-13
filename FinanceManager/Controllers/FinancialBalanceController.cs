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
            return View("ManageView");
        }

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
    }
}