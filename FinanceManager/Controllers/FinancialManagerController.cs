using Domain.Models;
using Domain.Repository;
using System;
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

            return View("AddAmontView");
        }

        public virtual ActionResult AddOutgoingAmount(double? Amount, string Description, long TypeID)
        {
            _outgoingRepository.Add(new Outgoing()
            {
                Amount = Amount.Value,
                Date = DateTime.Now,
                Description = Description,
                TypeID = TypeID
            });
            _outgoingRepository.CommitChanges();

            return RedirectToAction("AddAmontView");
        }

        public virtual ActionResult AddIncommingAmount(double? Amount, string Description, long SourceID)
        {
            _incomeRepository.Add(new Income()
            {
                Amount = Amount.GetValueOrDefault(0),
                Date = DateTime.Now,
                Description = Description,
                SourceID = SourceID
            });
            _incomeRepository.CommitChanges();

            return RedirectToAction("AddAmontView");
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

            return RedirectToAction("AddAmontView");
        }

        public virtual ActionResult AddIncomimngType(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                _sourceOfAmountRepository.Add(new SourceOfAmount()
                {
                    Name = Name
                });
                _sourceOfAmountRepository.CommitChanges();
            }

            return RedirectToAction("AddAmontView");
        }
    }
}