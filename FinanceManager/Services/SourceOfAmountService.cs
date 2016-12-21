using Domain.Models;
using Domain.Repository;
using System.Collections.Generic;

namespace FinanceManager.Services
{
    public class SourceOfAmountService
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
    }
}