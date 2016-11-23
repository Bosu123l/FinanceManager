using Domain.Models;
using Domain.Repository;
using FinanceManager.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class FinancialManagerController : Controller
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly SourceOfAmountRepository _sourceOfAmountRepository;
        private readonly OutgoingRepository _outgoingRepository;
        private readonly TypeOfOutgoingRepository _typeOfOutgoingRepository;

        public FinancialManagerController(IncomeRepository incomeRepository, SourceOfAmountRepository sourceOfAmountRepository,
            OutgoingRepository outgoingRepository, TypeOfOutgoingRepository typeOfOutgoingRepository)
        {
            _incomeRepository = incomeRepository;
            _sourceOfAmountRepository = sourceOfAmountRepository;
            _outgoingRepository = outgoingRepository;
            _typeOfOutgoingRepository = typeOfOutgoingRepository;
        }

        // GET: FinancialManager
        public virtual ActionResult Index()
        {
            ViewBag.IncomeType = _sourceOfAmountRepository.All();
            ViewBag.OutGoing = _typeOfOutgoingRepository.All();

            return View("ManageView");
        }

        public virtual ActionResult ManageAdd()
        {
            ViewBag.IncomeType = _sourceOfAmountRepository.All();
            ViewBag.OutGoing = _typeOfOutgoingRepository.All();

            GlobalViariables.LastRememberView = "ManageAdd";
            return View("AddAmontView");
        }

        public virtual ActionResult ManageEdit()
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

            ViewBag.Income = _incomeRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFromIncoming.Value.Date && x.Date.Value.Date <= GlobalViariables.DateToIncoming.Value.Date).ToList();

            ViewBag.Outgoing = _outgoingRepository.FilterBy(x => x.Date.Value.Date >= GlobalViariables.DateFromOutgoing.Value.Date && x.Date.Value.Date <= GlobalViariables.DateToOutgoing.Value.Date).ToList();

            ViewBag.IncomeType = _sourceOfAmountRepository.All();
            ViewBag.OutGoingType = _typeOfOutgoingRepository.All();

            GlobalViariables.LastRememberView = "ManageEdit";

            return View("EditAmontView");
        }

        public virtual ActionResult AddOutgoingAmount(double? Amount, string Description, long? TypeID)
        {
            _outgoingRepository.Add(new Outgoing()
            {
                Amount = Amount.Value,
                Date = DateTime.Now,
                Description = Description,
                TypeID = TypeID.Value
            });
            _outgoingRepository.CommitChanges();

            return RedirectToAction("ManageAdd");
        }
        [SubmitButtonSelector(Name = "DeleteOutgoing")]
        public virtual ActionResult RemoveOutgoingAmount(Outgoing outgoing)
        {
            var tempOutgoing = _outgoingRepository.GetById(outgoing.ID);
            _outgoingRepository.Delete(tempOutgoing);

            _outgoingRepository.CommitChanges();
            return RedirectToAction("ManageEdit");
        }

        [SubmitButtonSelector(Name = "EditOutgoing")]
        public virtual ActionResult EditOutgoingAmount(Outgoing outgoing)
        {
            var tempOutgoing = _outgoingRepository.GetById(outgoing.ID);

            if (outgoing.Description != tempOutgoing.Description && outgoing.Description != null)
            {
                tempOutgoing.Description = outgoing.Description;
            }
            if (outgoing.Amount != tempOutgoing.Amount && outgoing.Amount != 0)
            {
                tempOutgoing.Amount = outgoing.Amount;
            }
            if (outgoing.Date != tempOutgoing.Date && outgoing.Date != null)
            {
                tempOutgoing.Date = outgoing.Date;
            }
            if (outgoing.TypeID != tempOutgoing.TypeID)
            {
                tempOutgoing.TypeID = outgoing.TypeID;
            }

            _outgoingRepository.Update(tempOutgoing);

            _outgoingRepository.CommitChanges();

            return RedirectToAction("ManageEdit");
        }

        [SubmitButtonSelector(Name = "DeleteIncome")]
        public virtual ActionResult RemoveIncomigAmount(Income income)
        {
            var tempIncome = _incomeRepository.GetById(income.ID);
            _incomeRepository.Delete(tempIncome);

            _incomeRepository.CommitChanges();
            return RedirectToAction("ManageEdit");
        }
        [SubmitButtonSelector(Name = "EditIncome")]
        public virtual ActionResult EditIncomingAmount(Income income)
        {
            var tempIncome = _incomeRepository.GetById(income.ID);

            if (income.Description != tempIncome.Description && income.Description != null)
            {
                tempIncome.Description = income.Description;
            }
            if (income.Amount != tempIncome.Amount && income.Amount != 0)
            {
                tempIncome.Amount = income.Amount;
            }
            if (income.Date != tempIncome.Date && income.Date != null)
            {
                tempIncome.Date = income.Date;
            }
            if (income.SourceID != tempIncome.SourceID)
            {
                tempIncome.SourceID = income.SourceID;
            }

            _incomeRepository.Update(tempIncome);

            _incomeRepository.CommitChanges();
            return RedirectToAction("ManageEdit");
        }

        public virtual ActionResult AddIncommingAmount(double? Amount, string Description, long? SourceID)
        {
            _incomeRepository.Add(new Income()
            {
                Amount = Amount.GetValueOrDefault(0),
                Date = DateTime.Now,
                Description = Description,
                SourceID = SourceID.Value
            });
            _incomeRepository.CommitChanges();

            return RedirectToAction("ManageAdd");
        }

        public virtual ActionResult AddOutgointType(string typeOfOutgoing)
        {
            if (!string.IsNullOrEmpty(typeOfOutgoing))
            {
                _typeOfOutgoingRepository.Add(new TypeOfOutgoing()
                {
                    Name = typeOfOutgoing
                });
                _typeOfOutgoingRepository.CommitChanges();
            }

            return RedirectToAction(GlobalViariables.LastRememberView);
        }

        public virtual ActionResult AddIncomimngType(string IncomimngType)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                _sourceOfAmountRepository.Add(new SourceOfAmount()
                {
                    Name = IncomimngType
                });
                _sourceOfAmountRepository.CommitChanges();
            }

            return RedirectToAction(GlobalViariables.LastRememberView);
        }

        public virtual ActionResult ShowBetwenDate(DateTime? dateFrom, DateTime? dateTo)
        {
            IncomingInSpecificTime(dateFrom.Value, dateTo.Value);
            OutgoingInSpecificTime(dateFrom.Value, dateTo.Value);

            return RedirectToAction(GlobalViariables.LastRememberView);
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