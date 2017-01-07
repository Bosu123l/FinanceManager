using System;
using System.Collections.Generic;
using Income = FinanceManager.Entities.Income;

namespace FinanceManager.Services.Interfaces
{
    public interface IIncomeService
    {
        Income GetIncome(long? id, string userId);

        bool RemoveIncome(long? id);
        Income UpdateIncome(Income income);
        Income AddIncome(Income income);

        double SumOfIncoming(string userId);

        double SumOfIncomingByNumberOfDays(int days, string userId);

        double SumOfIncomingByNumberOfWeeks(int weeks, string userId);

        double SumOfIncomingByNumberOfMonth(int month, string userId);

        double SumOfIncomingByLastOperations(int count, string userId);

        double SumOfIncoming(DateTime firstDateTime, DateTime secondDateTime, string userId);

        IEnumerable<Income> GetIncomes(DateTime firstDateTime, DateTime secondDateTime, string userId);

        IEnumerable<Income> GetIncomesByNumberOfDays(int days, string userId);

        IEnumerable<Income> GetIncomesByNumberOfWeeks(int weeks, string userId);

        IEnumerable<Income> GetIncomeByNumberOfMonth(int month, string userId);

        IEnumerable<Income> GetIncomes(string userId);

        IEnumerable<Income> GetIncomesByLastOperations(int count, string userId);
    }
}