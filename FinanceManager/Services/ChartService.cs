using FinanceManager.Entities;
using FinanceManager.Entities.Models;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
{
    public class ChartService : IChartService
    {
        private readonly IIncomeService _incomeService;
        private readonly IOutGoingService _outGoingService;
        private readonly ISourceOfAmountService _sourceOfAmountService;
        private readonly ITypeOfOutgoingService _typeOfOutgoingService;
        public ChartService(IIncomeService incomeService, IOutGoingService outGoingService, ISourceOfAmountService sourceOfAmountService, ITypeOfOutgoingService typeOfOutgoingService)
        {
            _incomeService = incomeService;
            _outGoingService = outGoingService;
            _sourceOfAmountService = sourceOfAmountService;
            _typeOfOutgoingService = typeOfOutgoingService;
        }

        #region Incomings

        public IEnumerable<SumOfAmountInType> GetAllSumsInSpecficIncomeType(string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomes(userId), userId);
        }

        public IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfDays(int days, string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomesByNumberOfDays(days, userId), userId);
        }

        public IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfWeeks(int weeks, string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomesByNumberOfWeeks(weeks, userId), userId);
        }

        public IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfMonths(int month, string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomeByNumberOfMonth(month, userId), userId);
        }

        public IEnumerable<SumOfAmountInType> SumsInSpecficIncomeByLastOperations(int count, string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomesByLastOperations(count, userId), userId);
        }

        public IEnumerable<SumOfAmountInType> SumsInSpecficIncomeByDate(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return PrepereSumOfAmountInTypes(_incomeService.GetIncomes(firstDateTime, secondDateTime, userId), userId);
        }

        private IEnumerable<SumOfAmountInType> PrepereSumOfAmountInTypes(IEnumerable<Income> incomes, string userId)
        {
            var tempSumOfAmount = new List<SumOfAmountInType>();

            foreach (var sourceType in _sourceOfAmountService.GetSourceOfAmounts(userId))
            {
                var tempSum = new SumOfAmountInType
                {
                    SourceOfAmount = sourceType,
                    Sum = incomes.Where(x => x.SourceId.Equals(sourceType.Id)).Sum(x => x.Amount)
                };
                tempSumOfAmount.Add(tempSum);
            }

            return tempSumOfAmount;
        }

        #endregion Incomings

        #region Outgoing

        public IEnumerable<SumOfAmountOutgoingType> GetAllSumsInSpecficOutgoingType(string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutGoings(userId), userId);
        }

        public IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfDays(int days, string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutgoingsByNumberOfDays(days, userId),userId);
        }

        public IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfWeeks(int weeks, string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutgoingsByNumberOfWeeks(weeks, userId), userId);
        }

        public IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfMonths(int month, string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutgoingsByNumberOfMonth(month, userId), userId);
        }

        public IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingByLastOperations(int count, string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutgoingsByLastOperations(count, userId), userId);
        }

        public IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingByDate(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return PrepereSumOfAmountInTypes(_outGoingService.GetOutGoings(firstDateTime, secondDateTime, userId), userId);
        }

        private IEnumerable<SumOfAmountOutgoingType> PrepereSumOfAmountInTypes(IEnumerable<Outgoing> outgoings,string userId)
        {
            var tempSumOfAmount = new List<SumOfAmountOutgoingType>();

            foreach (var outgoingType in _typeOfOutgoingService.GetTypeOfOutgoings(userId))
            {
                var tempSum = new SumOfAmountOutgoingType
                {
                    TypeOfOutgoing = outgoingType,
                    Sum = outgoings.Where(x => x.TypeId.Equals(outgoingType.Id)).Sum(x => x.Amount)
                };
                tempSumOfAmount.Add(tempSum);
            }

            return tempSumOfAmount;
        }

        #endregion Outgoing
    }
}