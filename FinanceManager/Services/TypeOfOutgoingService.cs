using Domain.Models;
using Domain.Repository;
using System;
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

        public TypeOfOutgoing AddTypeOfOutgoing(TypeOfOutgoing typeOfOutgoing)
        {
            TypeOfOutgoing tempTypeOfAmount;
            try
            {
                tempTypeOfAmount = _outgoingRepository.Add(typeOfOutgoing);
                _outgoingRepository.CommitChanges(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempTypeOfAmount;
        }
    }
}