using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinanceManager.Models;

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



        public ActionResult SetDateRange(DateTime? dateOf, DateTime? dateTo)
        {
            if (dateOf != null)
            {
                GlobalViariables.DateFromIncoming = dateOf.Value;
                ViewBag.dateOf = dateOf.Value;
            }

            if (dateTo != null)
            {
                GlobalViariables.DateToIncoming = dateTo.Value;
                ViewBag.dateTo = dateTo.Value;
            }



            return RedirectToAction("Index");
        }
        public ActionResult SetDateRangeForOutcoming(DateTime? dateOf,DateTime? dateTo)
        {
            if (dateOf != null)
            {
                GlobalViariables.DateFromOutgoing = dateOf.Value;
                ViewBag.dateOfOutcomming = dateOf.Value;
            }

            if (dateTo != null)
            {
                GlobalViariables.DateToOutgoing = dateTo.Value;
                ViewBag.dateToOutcomming= dateTo.Value;
            }
            return RedirectToAction("Index");
        }
        // GET: Financial
        public virtual ActionResult Index()
        {
            var now = DateTime.Now;
            if (GlobalViariables.DateFromIncoming == null)
            {
                GlobalViariables.DateFromIncoming = new DateTime(now.Year, now.Month, 1);
            }
            if (GlobalViariables.DateToIncoming == null)
            {
                GlobalViariables.DateToIncoming = new DateTime(now.Year, now.Month + 1, 1);
            }


            var incomeInThisMounth =
                _incomeRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFromIncoming.Value.Date && x.Date.Value.Date <= GlobalViariables.DateToIncoming.Value.Date).ToList();

            ViewBag.Income = incomeInThisMounth;

            ViewBag.Outgoing =
                _outgoingRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFromIncoming.Value.Date && x.Date.Value.Date <= GlobalViariables.DateToIncoming.Value.Date).ToList();

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

        public ActionResult AddOutgoingAmount(double? Amount, string Description, long TypeID)
        {
            _outgoingRepository.Add(new Outgoing()
            {
                Amount = Amount.Value,
                Date = DateTime.Now,
                Description = Description,
                TypeID = TypeID
            });
            _outgoingRepository.CommitChanges();
          
            return RedirectToAction("Index");
        }

        public ActionResult AddIncomimngType(string Name)
        {

            if (!string.IsNullOrEmpty(Name))
            {
                _sourceOfAmountRepository.Add(new SourceOfAmount()
                {
                    Name = Name
                });
                _sourceOfAmountRepository.CommitChanges();
            }


            return RedirectToAction("Index");
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
        public double SumOfOutgoing(IEnumerable<Outgoing> outgoing)
        {
            if (outgoing != null)
            {
                return outgoing.Sum(x => x.Amount);
            }
            else
            {
                return 0;
            }
        }
    }
}