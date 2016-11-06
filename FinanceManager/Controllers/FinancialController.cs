using Domain.Models;
using Domain.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class FinancialController : Controller
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly SourceOfAmountRepository _sourceOfAmountRepository;
        private readonly OutgoingRepository _outgoingRepository;
        private readonly TypeOfOutgoingRepository _typeOfOutgoingRepository;

        public FinancialController(IncomeRepository incomeRepository, SourceOfAmountRepository sourceOfAmountRepository,
            OutgoingRepository outgoingRepository, TypeOfOutgoingRepository typeOfOutgoingRepository)
        {
            _incomeRepository = incomeRepository;
            _sourceOfAmountRepository = sourceOfAmountRepository;
            _outgoingRepository = outgoingRepository;
            _typeOfOutgoingRepository = typeOfOutgoingRepository;
        }

        // GET: Financial
        public virtual ActionResult Index()
        {
            var incomeInThisMounth =
                _incomeRepository.FilterBy(x => x.Date.Value.Date.Month.Equals(DateTime.Now.Month)).ToList();

            ViewBag.Income = incomeInThisMounth;

            ViewBag.Outgoing =
                _outgoingRepository.FilterBy(x => x.Date.Value.Date.Month.Equals(DateTime.Now.Month)).ToList();

            ViewBag.Type = _sourceOfAmountRepository.All();
            ViewBag.OutType = _typeOfOutgoingRepository.All();

            return View("ManageView");
        }

        public ActionResult AddAmount(double? Amount, string Description, long SourceID)
        {
            _incomeRepository.Add(new Income()
            {
                Amount = Amount.GetValueOrDefault(0),
                Date = DateTime.Now,
                Description = Description,
                SourceID = SourceID
            });
            _incomeRepository.CommitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddOutgoingAmount()
        {
            _outgoingRepository.Add(new Outgoing()
            {
                Amount = 0,
                Date = DateTime.Now,
                Description = string.Empty,
                TypeID = 0
            });
            _outgoingRepository.CommitChanges();

            return RedirectToAction("Index");
        }
    }
}