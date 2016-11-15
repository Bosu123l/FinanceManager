using Domain.Models;
using Domain.Repository;
using FinanceManager.Models;
using System;
using System.Collections.Generic;
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

        private double _incommingSum;
        private double _outgoingSum;

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

        public ActionResult SetDateRangeForOutcoming(DateTime? dateOf, DateTime? dateTo)
        {
            if (dateOf != null)
            {
                GlobalViariables.DateFromOutgoing = dateOf.Value;
                ViewBag.dateOfOutcomming = dateOf.Value;
            }

            if (dateTo != null)
            {
                GlobalViariables.DateToOutgoing = dateTo.Value;
                ViewBag.dateToOutcomming = dateTo.Value;
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
                GlobalViariables.DateToIncoming = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            }
            if (GlobalViariables.DateFromOutgoing == null)
            {
                GlobalViariables.DateFromOutgoing = new DateTime(now.Year, now.Month, 1);
            }
            if (GlobalViariables.DateToOutgoing == null)
            {
                GlobalViariables.DateToOutgoing = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            }

            ViewBag.IncomeSum = GetIncomes(GlobalViariables.DateFromIncoming.Value.Date, GlobalViariables.DateToIncoming.Value.Date).Sum(x => x.Amount);

            ViewBag.OutgoingSum = GetOutgoings(GlobalViariables.DateFromOutgoing.Value.Date, GlobalViariables.DateToOutgoing.Value.Date).Sum(x => x.Amount);

            ViewBag.Type = _sourceOfAmountRepository.All();
            ViewBag.OutType = _typeOfOutgoingRepository.All();

            return View("ManageView");
        }

        public IEnumerable<Income> GetIncomes(DateTime dateFrom, DateTime dateTo)
        {
            var incomes = _incomeRepository.FilterBy(x => x.Date.Value.Date >= dateFrom.Date && x.Date.Value.Date <= dateTo.Date).ToList();
            ViewBag.Income = incomes;
            return incomes;
        }

        public IEnumerable<Outgoing> GetOutgoings(DateTime dateFrom, DateTime dateTo)
        {
            var ougoings = _outgoingRepository.FilterBy(x => x.Date.Value.Date >= dateFrom.Date && x.Date.Value.Date <= dateTo.Date).ToList();
            ViewBag.Outgoing = ougoings;

            return ougoings;
        }

        public ActionResult Incomming(DateTime dateFrom, DateTime dateTo)
        {
            GlobalViariables.DateFromIncoming = dateFrom;
            GlobalViariables.DateToIncoming = dateTo;


            return RedirectToAction("Index");
        }

        public ActionResult Outgoing(DateTime dateFrom, DateTime dateTo)
        {
            GlobalViariables.DateFromOutgoing = dateFrom;
            GlobalViariables.DateToOutgoing = dateTo;


            return RedirectToAction("Index");
        }

        public ActionResult CalculateFromSpecificYear(string year)
        {
            IncommingInSpecificYear(year);
            OutgoingInSpecificYear(year);

            return RedirectToAction("Index");
        }
        public ActionResult CalculateFromSpecificMonth(DateTime? selectedMonth)
        {

            IncommigInSpecificMonth(selectedMonth.Value);
            OutgoingInSpecificMonth(selectedMonth.Value);


            return RedirectToAction("Index");
        }
        public ActionResult CalculateBetwenDate(DateTime? dateFrom, DateTime? dateTo)
        {
            return RedirectToAction("Index");
        }
        public void IncommingInSpecificYear(string year)
        {
            DateTime time = new DateTime(Convert.ToInt32(year), 1, 1);

            var firstOfYear = new DateTime(time.Year, 1, 1);
            var lastOfYear = new DateTime(time.Year, 12, DateTime.DaysInMonth(time.Year, 12));

            GlobalViariables.DateFromIncoming = firstOfYear;
            GlobalViariables.DateToIncoming = lastOfYear;

        }

        public void OutgoingInSpecificYear(string year)
        {
            DateTime time = new DateTime(Convert.ToInt32(year), 1, 1);

            var firstOfYear = new DateTime(time.Year, 1, 1);
            var lastOfYear = new DateTime(time.Year, 12, DateTime.DaysInMonth(time.Year, 12));

            GlobalViariables.DateFromOutgoing = firstOfYear;
            GlobalViariables.DateToOutgoing = lastOfYear;

        }

        public void IncommigInSpecificMonth(DateTime date)
        {
            var firstOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastOfMonthr = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            GlobalViariables.DateFromIncoming = firstOfMonth;
            GlobalViariables.DateToIncoming = lastOfMonthr;


        }

        public void OutgoingInSpecificMonth(DateTime date)
        {
            var firstOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastOfMonthr = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            GlobalViariables.DateFromOutgoing = firstOfMonth;
            GlobalViariables.DateToOutgoing = lastOfMonthr;

        }

        public ActionResult OutgoingFromBegining()
        {
            var firstDate = _outgoingRepository.All().Select(x => x.Date).OrderByDescending(x => x.Value).FirstOrDefault();
            var now = DateTime.Now;

            GlobalViariables.DateFromOutgoing = firstDate;
            GlobalViariables.DateToOutgoing = now;


            return RedirectToAction("Index");
        }

        public ActionResult IncomingFromBegining()
        {
            var firstDate = _incomeRepository.All().Select(x => x.Date).OrderByDescending(x => x.Value).FirstOrDefault();
            var now = DateTime.Now;

            GlobalViariables.DateFromIncoming = firstDate;
            GlobalViariables.DateToIncoming = now;


            return RedirectToAction("Index");
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