using FinanceManager.Entities.Models;
using System;
using System.Collections.Generic;
using Income = FinanceManager.Entities.Income;

namespace FinanceManager.Services.Interfaces
{
    public interface IIncomeService
    {
        Income AddIncome(Income income);

        Income GetIncome(long? id, string userId);

        IEnumerable<Income> GetIncomeByNumberOfMonth(int month, string userId);

        IEnumerable<Income> GetIncomes(string userId);

        IEnumerable<Income> GetIncomes(DateTime firstDateTime, DateTime secondDateTime, string userId);

        IEnumerable<Income> GetIncomesByLastOperations(int count, string userId);

        IEnumerable<Income> GetIncomesByNumberOfDays(int days, string userId);

        IEnumerable<Income> GetIncomesByNumberOfWeeks(int weeks, string userId);

        bool RemoveIncome(long? id);

        double SumOfIncoming(string userId);

        double SumOfIncoming(DateTime firstDateTime, DateTime secondDateTime, string userId);

        double SumOfIncomingByLastOperations(int count, string userId);

        double SumOfIncomingByNumberOfDays(int days, string userId);

        double SumOfIncomingByNumberOfMonth(int month, string userId);

        double SumOfIncomingByNumberOfWeeks(int weeks, string userId);

        Income UpdateIncome(Income income);
    }
}