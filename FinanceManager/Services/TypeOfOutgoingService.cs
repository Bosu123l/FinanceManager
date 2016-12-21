using Domain.Models;
using Domain.Repository;
using System.Collections.Generic;

namespace FinanceManager.Services
{
    public class TypeOfOutgoingService
    {
        private readonly TypeOfOutgoingRepository _outgoingRepository;

        public TypeOfOutgoingService(TypeOfOutgoingRepository outgoingRepository)
        {
            _outgoingRepository = outgoingRepository;
        }

        public IEnumerable<TypeOfOutgoing> GetTypeOfOutgoings()
        {
            return _outgoingRepository.All();
        }
    }
}