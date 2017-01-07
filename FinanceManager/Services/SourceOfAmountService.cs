using FinanceManager.Entities;
using FinanceManager.Entities.Context;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
{
    public class SourceOfAmountService : ISourceOfAmountService
    {
        private readonly FinanceManagerContext _financeManagerContext;

        public SourceOfAmountService(FinanceManagerContext financeManagerContext)
        {
            _financeManagerContext = financeManagerContext;
        }

        public IEnumerable<SourceOfAmount> GetSourceOfAmounts(string userId)
        {
            return _financeManagerContext.SourceOfAmounts.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public SourceOfAmount AddSourceOfAmount(SourceOfAmount sourceOfAmount)
        {
            SourceOfAmount tempSourceOfAmount;
            try
            {
                tempSourceOfAmount = _financeManagerContext.SourceOfAmounts.Add(sourceOfAmount);
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                tempSourceOfAmount = null;
            }
            return tempSourceOfAmount;
        }
    }
}