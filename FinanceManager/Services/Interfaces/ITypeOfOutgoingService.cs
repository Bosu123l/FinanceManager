using System.Collections.Generic;
using TypeOfOutgoing = FinanceManager.Entities.TypeOfOutgoing;

namespace FinanceManager.Services.Interfaces
{
    public interface ITypeOfOutgoingService
    {
        IEnumerable<TypeOfOutgoing> GetTypeOfOutgoings(string userId);

        TypeOfOutgoing AddTypeOfOutgoing(TypeOfOutgoing typeOfOutgoing);
    }
}