using System;
using System.Collections.Generic;
using Outgoing = FinanceManager.Entities.Outgoing;

namespace FinanceManager.Services.Interfaces
{
    public interface IOutGoingService
    {
        Outgoing GetOutGoing(long id, string userId);

        bool RemoveOutGoing(long id, string userId);

        Outgoing UpdateOutgoing(Outgoing outgoing);

        Outgoing AddOutgoing(Outgoing outgoing);

        double SumOfOutgoings(string userId);

        double SumOfOutgoings(DateTime firstDateTime, DateTime secondDateTime, string userId);

        double SumOfOutgoingsByNumberOfDays(int days, string userId);

        double SumOfOutgoingsByNumberOfWeeks(int weeks, string userId);

        double SumOfOutgoingsByNumberOfMonth(int month, string userId);

        double SumOfOutgoingsByLastOperations(int count, string userId);

        IEnumerable<Outgoing> GetOutGoings(DateTime firstDateTime, DateTime secondDateTime, string userId);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfDays(int days, string userId);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfWeeks(int weeks, string userId);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfMonth(int month, string userId);

        IEnumerable<Outgoing> GetOutGoings(string userId);

        IEnumerable<Outgoing> GetOutgoingsByLastOperations(int count, string userId);
    }
}