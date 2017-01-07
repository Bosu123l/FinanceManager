using System.Collections.Generic;
using SourceOfAmount = FinanceManager.Entities.SourceOfAmount;

namespace FinanceManager.Services.Interfaces
{
    public interface ISourceOfAmountService
    {
        IEnumerable<SourceOfAmount> GetSourceOfAmounts(string userId);

        SourceOfAmount AddSourceOfAmount(SourceOfAmount sourceOfAmount);
    }
}