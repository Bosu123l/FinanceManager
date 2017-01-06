using Domain.Models;
using Domain.Repository;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace FinanceManager.Services
{
    public class SourceOfAmountService : ISourceOfAmountService
    {
        private readonly SourceOfAmountRepository _sourceOfAmountRepository;

        public SourceOfAmountService(SourceOfAmountRepository sourceOfAmountRepository)
        {
            _sourceOfAmountRepository = sourceOfAmountRepository;
        }

        public IEnumerable<SourceOfAmount> GetSourceOfAmounts()
        {
            return _sourceOfAmountRepository.All();
        }

        public SourceOfAmount AddSourceOfAmount(SourceOfAmount sourceOfAmount)
        {
            SourceOfAmount tempSourceOfAmount;
            try
            {
                tempSourceOfAmount = _sourceOfAmountRepository.Add(sourceOfAmount);
                _sourceOfAmountRepository.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempSourceOfAmount;
        }
    }
}