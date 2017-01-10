using FinanceManager.Entities;
using FinanceManager.Entities.Context;
using FinanceManager.Entities.Models;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FinanceManager.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly FinanceManagerContext _financeManagerContext;

        public IncomeService(FinanceManagerContext financeManagerContext)
        {
            _financeManagerContext = financeManagerContext;
        }

        public Income GetIncome(long? id, string userId)
        {
            return _financeManagerContext.Incomes.SingleOrDefault(x => x.Id == id.Value && x.UserId.Equals(userId));
        }

        public bool RemoveIncome(long? id)
        {
            bool success;
            try
            {
                _financeManagerContext.Incomes.Remove(_financeManagerContext.Incomes.SingleOrDefault(x => x.Id == id));

                _financeManagerContext.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        public Income UpdateIncome(Income income)
        {
            Income tempIncome;

            try
            {
                tempIncome = _financeManagerContext.Incomes.Attach(income);
                _financeManagerContext.Entry(tempIncome).State = EntityState.Modified;
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempIncome;
        }

        public Income AddIncome(Income income)
        {
            Income tempIncome;
            try
            {
                tempIncome = _financeManagerContext.Incomes.Add(income);
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempIncome;
        }

        public double SumOfIncoming(string userId)
        {
            return GetIncomes(userId).Sum(x => x.Amount);
        }

        public double SumOfIncomingByNumberOfDays(int days, string userId)
        {
            return GetIncomesByNumberOfDays(days, userId).Sum(x => x.Amount);
        }

        public double SumOfIncomingByNumberOfWeeks(int weeks, string userId)
        {
            return GetIncomesByNumberOfWeeks(weeks, userId).Sum(x => x.Amount);
        }

        public double SumOfIncomingByNumberOfMonth(int month, string userId)
        {
            return GetIncomeByNumberOfMonth(month, userId).Sum(x => x.Amount);
        }

        public double SumOfIncomingByLastOperations(int count, string userId)
        {
            return GetIncomesByLastOperations(count, userId).Sum(x => x.Amount);
        }

        public double SumOfIncoming(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return GetIncomes(firstDateTime, secondDateTime, userId).Sum(x => x.Amount);
        }

        public IEnumerable<Income> GetIncomes(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return _financeManagerContext.Incomes.Where(x => x.Date.Value >= firstDateTime.Date && x.Date.Value <= secondDateTime.Date && x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Income> GetIncomesByNumberOfDays(int days, string userId)
        {
            var daysAgo = DateTime.Now.AddDays(days * -1);

            return GetIncomes(daysAgo, DateTime.Now, userId);
        }

        public IEnumerable<Income> GetIncomesByNumberOfWeeks(int weeks, string userId)
        {
            var weeksAgo = DateTime.Now.AddDays((weeks * 7) * -1);

            return GetIncomes(weeksAgo, DateTime.Now, userId);
        }

        public IEnumerable<Income> GetIncomeByNumberOfMonth(int month, string userId)
        {
            var monthsAgo = DateTime.Now.AddMonths(month * -1);

            return GetIncomes(monthsAgo, DateTime.Now, userId);
        }

        public IEnumerable<Income> GetIncomes(string userId)
        {
            return _financeManagerContext.Incomes.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Income> GetIncomesByLastOperations(int count, string userId)
        {
            return _financeManagerContext.Incomes.Where(x => x.UserId.Equals(userId)).Take(count).ToList();
        }

     
    }
}