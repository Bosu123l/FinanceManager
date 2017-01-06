using Domain.Models;
using System.Collections.Generic;

namespace FinanceManager.Services.Interfaces
{
    public interface ITypeOfOutgoingService
    {
        IEnumerable<TypeOfOutgoing> GetTypeOfOutgoings();

        TypeOfOutgoing AddTypeOfOutgoing(TypeOfOutgoing typeOfOutgoing);
    }
}