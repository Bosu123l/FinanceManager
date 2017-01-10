using FinanceManager.Entities.Models;
using System;
using System.Collections.Generic;

namespace FinanceManager.Services.Interfaces
{
    public interface IChartService
    {
        IEnumerable<SumOfAmountInType> GetAllSumsInSpecficIncomeType(string userId);

        IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfDays(int days, string userId);

        IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfWeeks(int weeks, string userId);

        IEnumerable<SumOfAmountInType> SumsInSpecficIncomeTypeNumberOfMonths(int month, string userId);

        IEnumerable<SumOfAmountInType> SumsInSpecficIncomeByLastOperations(int count, string userId);

        IEnumerable<SumOfAmountInType> SumsInSpecficIncomeByDate(DateTime firstDateTime, DateTime secondDateTime, string userId);

        IEnumerable<SumOfAmountOutgoingType> GetAllSumsInSpecficOutgoingType(string userId);

        IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfDays(int days, string userId);

        IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfWeeks(int weeks, string userId);

        IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingTypeNumberOfMonths(int month, string userId);

        IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingByLastOperations(int count, string userId);

        IEnumerable<SumOfAmountOutgoingType> SumsInSpecficOutgoingByDate(DateTime firstDateTime, DateTime secondDateTime, string userId);
    }
}