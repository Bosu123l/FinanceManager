using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinanceManager.Models;

namespace FinanceManager.Controllers
{
    [Authorize]
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

      

        public ActionResult SetDateRange(DateTime? dateOf, DateTime? dateTo)
        {
            if (dateOf != null)
            {
                GlobalViariables.DateFrom = dateOf.Value;
                ViewBag.dateOf = dateOf.Value;
            }

            if (dateTo != null)
            {
                GlobalViariables.DateTo = dateTo.Value;
                ViewBag.dateTo = dateTo.Value;
            }
              


            return RedirectToAction("Index");
        }
        // GET: Financial
        public virtual ActionResult Index()
        {
            var now = DateTime.Now;
            if (GlobalViariables.DateFrom == null)
            {
                GlobalViariables.DateFrom = new DateTime(now.Year, now.Month, 1);
            }
            if (GlobalViariables.DateTo == null)
            {
                GlobalViariables.DateTo = new DateTime(now.Year, now.Month + 1, 1);
            }



            var incomeInThisMounth =
                _incomeRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFrom.Value.Date && x.Date.Value.Date <= GlobalViariables.DateTo.Value.Date).ToList();

            ViewBag.Income = incomeInThisMounth;

            ViewBag.Outgoing =
                _outgoingRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFrom.Value.Date && x.Date.Value.Date <= GlobalViariables.DateTo.Value.Date).ToList();

            ViewBag.Type = _sourceOfAmountRepository.All();
            ViewBag.OutType = _typeOfOutgoingRepository.All();


            ViewBag.IncomeSum = SumOfIncome(incomeInThisMounth);


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
            Index();
            return null;
        }

        public double SumOfIncome(IEnumerable<Income> incomes)
        {

            if (incomes != null)
            {
                return incomes.Sum(x => x.Amount);

            }
            else
            {
                return 0;
            }

        }
    }
}