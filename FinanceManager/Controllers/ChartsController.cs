using FinanceManager.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    [System.Web.Mvc.Authorize]
    [System.Web.Mvc.Route("api/[controller]")]
    public class ChartsController : Controller
    {
        private readonly IChartService _chartService;
        private readonly string _idLoggedUser;

        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;

            try
            {
                _idLoggedUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id;
            }
            catch (Exception ex)
            {
                _idLoggedUser = string.Empty;
            }
        }

        #region Incomes

        [System.Web.Mvc.Route("/GetSumInSpecificIncomeType")]
        [HttpGet]
        public ActionResult GetSumInSpecificIncomeType()
        {
            return Json(_chartService.GetAllSumsInSpecficIncomeType(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumInSpecificIncomeTypeByNumberOfDays/{days}")]
        [HttpGet]
        public ActionResult GetSumInSpecificIncomeTypeByNumberOfDays(int? days)
        {
            return Json(_chartService.SumsInSpecficIncomeTypeNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficIncomeTypeNumberOfWeeks/{weeks}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficIncomeTypeNumberOfWeeks(int? weeks)
        {
            return Json(_chartService.SumsInSpecficIncomeTypeNumberOfWeeks(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficIncomeTypeNumberOfMonths/{month}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficIncomeTypeNumberOfMonths(int? month)
        {
            return Json(_chartService.SumsInSpecficIncomeTypeNumberOfMonths(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficIncomeByLastOperations/{count}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficIncomeByLastOperations(int? count)
        {
            return Json(_chartService.SumsInSpecficIncomeByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficIncomeByDate/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficIncomeByDate(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return Json(_chartService.SumsInSpecficIncomeByDate(firstDateTime, secondDateTime, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        #endregion Incomes

        #region Outgoings

        [System.Web.Mvc.Route("/GetSumInSpecificOutgoingType")]
        [HttpGet]
        public ActionResult GetSumInSpecificOutgoingType()
        {
            return Json(_chartService.GetAllSumsInSpecficOutgoingType(_idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumInSpecificOutgoingTypeByNumberOfDays/{days}")]
        [HttpGet]
        public ActionResult GetSumInSpecificOutgoingTypeByNumberOfDays(int? days)
        {
            return Json(_chartService.SumsInSpecficOutgoingTypeNumberOfDays(days.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficOutgoingTypeNumberOfWeeks/{weeks}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficOutgoingTypeNumberOfWeeks(int? weeks)
        {
            return Json(_chartService.SumsInSpecficOutgoingTypeNumberOfWeeks(weeks.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficOutgoingTypeNumberOfMonths/{month}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficOutgoingTypeNumberOfMonths(int? month)
        {
            return Json(_chartService.SumsInSpecficOutgoingTypeNumberOfMonths(month.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficOutgoingByLastOperations/{count}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficOutgoingByLastOperations(int? count)
        {
            return Json(_chartService.SumsInSpecficOutgoingByLastOperations(count.GetValueOrDefault(0), _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        [Route("/GetSumsInSpecficOutgoingByDate/{firstDateTime}/{secondDateTime}")]
        [HttpGet]
        public ActionResult GetSumsInSpecficOutgoingByDate(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return Json(_chartService.SumsInSpecficOutgoingByDate(firstDateTime, secondDateTime, _idLoggedUser), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}