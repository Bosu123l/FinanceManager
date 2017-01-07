using FinanceManager.Entities;
using FinanceManager.Entities.Context;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
{
    public class TypeOfOutgoingService : ITypeOfOutgoingService
    {
        private readonly FinanceManagerContext _financeManagerContext;

        public TypeOfOutgoingService(FinanceManagerContext financeManagerContext)
        {
            _financeManagerContext = financeManagerContext;
        }

        public IEnumerable<TypeOfOutgoing> GetTypeOfOutgoings(string userId)
        {
            return _financeManagerContext.TypeOfOutgoings.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public TypeOfOutgoing AddTypeOfOutgoing(TypeOfOutgoing typeOfOutgoing)
        {
            TypeOfOutgoing tempTypeOfAmount;
            try
            {
                tempTypeOfAmount = _financeManagerContext.TypeOfOutgoings.Add(typeOfOutgoing);
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                tempTypeOfAmount = null;
            }
            return tempTypeOfAmount;
        }
    }
}