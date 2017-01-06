using Domain.Models;
using System;
using System.Collections.Generic;

namespace FinanceManager.Services.Interfaces
{
    public interface IOutGoingService
    {
        Outgoing GetOutGoing(long id);

        bool RemoveOutGoing(long id);

        Outgoing UpdateOutgoing(Outgoing outgoing);

        Outgoing AddOutgoing(Outgoing outgoing);

        double SumOfOutgoings();

        double SumOfOutgoings(DateTime firstDateTime, DateTime secondDateTime);

        double SumOfOutgoingsByNumberOfDays(int days);

        double SumOfOutgoingsByNumberOfWeeks(int weeks);

        double SumOfOutgoingsByNumberOfMonth(int month);

        double SumOfOutgoingsByLastOperations(int count);

        IEnumerable<Outgoing> GetOutGoings(DateTime firstDateTime, DateTime secondDateTime);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfDays(int days);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfWeeks(int weeks);

        IEnumerable<Outgoing> GetOutgoingsByNumberOfMonth(int month);

        IEnumerable<Outgoing> GetOutGoings();

        IEnumerable<Outgoing> GetOutgoingsByLastOperations(int count);
    }
}