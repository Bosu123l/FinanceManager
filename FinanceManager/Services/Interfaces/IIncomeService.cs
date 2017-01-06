using Domain.Models;
using System;
using System.Collections.Generic;

namespace FinanceManager.Services.Interfaces
{
    public interface IIncomeService
    {
        Income GetIncome(long? id);

        bool RemoveIncome(long? id);

        Income UpdateIncome(Income income);

        Income AddIncome(Income income);

        double SumOfIncoming();

        double SumOfIncomingByNumberOfDays(int days);

        double SumOfIncomingByNumberOfWeeks(int weeks);

        double SumOfIncomingByNumberOfMonth(int month);

        double SumOfIncoming(DateTime firstDateTime, DateTime secondDateTime);

        double SumOfIncomingByLastOperations(int count);

        IEnumerable<Income> GetIncomes(DateTime firstDateTime, DateTime secondDateTime);

        IEnumerable<Income> GetIncomesByNumberOfDays(int days);

        IEnumerable<Income> GetIncomesByNumberOfWeeks(int weeks);

        IEnumerable<Income> GetIncomeByNumberOfMonth(int month);

        IEnumerable<Income> GetIncomes();

        IEnumerable<Income> GetIncomesByLastOperations(int count);
    }
}