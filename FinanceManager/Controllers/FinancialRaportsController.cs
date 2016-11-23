using Domain.Models;
using Domain.Repository;
using FinanceManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class FinancialRaportsController : Controller
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly OutgoingRepository _outgoingRepository;
        public FinancialRaportsController(IncomeRepository incomeRepository, OutgoingRepository outgoingRepository)
        {
            _incomeRepository = incomeRepository;
            _outgoingRepository = outgoingRepository;
        }
        // GET: FinancialRaports
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


            ViewBag.Income = GetIncomes(GlobalViariables.DateFromIncoming.Value.Date, GlobalViariables.DateToIncoming.Value.Date);

            ViewBag.OutgoingSum = GetOutgoings(GlobalViariables.DateFromOutgoing.Value.Date, GlobalViariables.DateToOutgoing.Value.Date);



            ViewBag.IncomeAmount = GetIncomes(GlobalViariables.DateFromIncoming.Value.Date, GlobalViariables.DateToIncoming.Value.Date).Select(x => x.Amount);

            GlobalViariables.LastRememberView = "ChartView";

            return View("ChartView");
        }
        public virtual ActionResult CreateLine()
        {

            List<string> yValues = new List<string>();
            List<string> xValues = new List<string>();

            var incomes = GetIncomes(GlobalViariables.DateFromIncoming.Value.Date, GlobalViariables.DateToIncoming.Value.Date);


            foreach (var income in incomes)
            {
                yValues.Add(income.Amount.ToString());
                xValues.Add(income.Date.Value.ToString("d"));
            }

            List<string> intList = new List<string>();


            IEnumerable test = intList;
            for (int i = 0; i < 4; i++)
            {
                intList.Add(i.ToString());

            }

            //Create bar chart
            var chart = new Chart(width: 600, height: 200)
            .AddSeries(chartType: "line",
                            xValue: xValues,
                            yValues: yValues)
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }
        public virtual ActionResult Raports()
        {


            return RedirectToAction("Index");
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
     public virtual ActionResult CalculateFromSpecificYear(string year)
        {
            IncommingInSpecificYear(year);
            OutgoingInSpecificYear(year);

            return RedirectToAction("Index");
        }

        public virtual ActionResult CalculateFromSpecificMonth(DateTime? selectedMonth)
        {
            IncommigInSpecificMonth(selectedMonth.Value);
            OutgoingInSpecificMonth(selectedMonth.Value);

            return RedirectToAction("Index");
        }

        public virtual ActionResult CalculateBetwenDate(DateTime? dateFrom, DateTime? dateTo)
        {
            IncomingInSpecificTime(dateFrom.Value, dateTo.Value);
            OutgoingInSpecificTime(dateFrom.Value, dateTo.Value);

            return RedirectToAction("Index");
        }

        public virtual ActionResult CalclateFromBegining()
        {
            IncomingFromBegining();
            OutgoingFromBegining();

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

        public void OutgoingFromBegining()
        {
            var firstDate = _outgoingRepository.All().Select(x => x.Date).OrderByDescending(x => x.Value).FirstOrDefault();
            var now = DateTime.Now;

            GlobalViariables.DateFromOutgoing = firstDate;
            GlobalViariables.DateToOutgoing = now;
        }

        public void IncomingFromBegining()
        {
            var firstDate = _incomeRepository.All().ToList().OrderBy(x => x.Date).FirstOrDefault().Date;
            var now = DateTime.Now;

            GlobalViariables.DateFromIncoming = firstDate;
            GlobalViariables.DateToIncoming = now;
        }

        public void IncomingInSpecificTime(DateTime dateFrom, DateTime dateTo)
        {
            GlobalViariables.DateFromIncoming = dateFrom;
            GlobalViariables.DateToIncoming = dateTo;
        }

        public void OutgoingInSpecificTime(DateTime dateFrom, DateTime dateTo)
        {
            GlobalViariables.DateFromOutgoing = dateFrom;
            GlobalViariables.DateToOutgoing = dateTo;
        }

    }
}