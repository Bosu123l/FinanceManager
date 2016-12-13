using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
{
    public class IncomeService
    {
        private readonly IncomeRepository _incomeRepository;

        public IncomeService(IncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public Income GetIncome(long? id)
        {
            return _incomeRepository.GetById(id.Value);
        }

        public bool RemoveIncome(long? id)
        {
            bool success;
            try
            {
                success = _incomeRepository.Delete(this.GetIncome(id));

                _incomeRepository.CommitChanges();
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
                tempIncome = _incomeRepository.Update(income);
                _incomeRepository.CommitChanges();
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
                tempIncome = _incomeRepository.Add(income);
                _incomeRepository.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempIncome;
        }

        public double SumOfIncoming()
        {
            return GetIncomes().Sum(x => x.Amount);
        }

        public double SumOfIncoming(DateTime firstDateTime, DateTime secondDateTime)
        {
            return GetIncomes(firstDateTime, secondDateTime).Sum(x => x.Amount);
        }

        public IEnumerable<Income> GetIncomes(DateTime firstDateTime, DateTime secondDateTime)
        {
            return _incomeRepository.FilterBy(x => x.Date.Value.Date >= secondDateTime.Date && x.Date.Value.Date <= secondDateTime.Date).ToList();
        }

        public IEnumerable<Income> GetIncomes()
        {
            return _incomeRepository.All().ToList();
        }
    }
}