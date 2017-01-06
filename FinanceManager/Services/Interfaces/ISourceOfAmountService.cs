using Domain.Models;
using System.Collections.Generic;

namespace FinanceManager.Services.Interfaces
{
    public interface ISourceOfAmountService
    {
        IEnumerable<SourceOfAmount> GetSourceOfAmounts();

        SourceOfAmount AddSourceOfAmount(SourceOfAmount sourceOfAmount);
    }
}