using FinanceManager.Entities;
using FinanceManager.Entities.Context;
using FinanceManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static System.Data.Entity.Core.Objects.EntityFunctions;

namespace FinanceManager.Services
{
    public class OutGoingService : IOutGoingService
    {
        private readonly FinanceManagerContext _financeManagerContext;

        public OutGoingService(FinanceManagerContext financeManagerContext)
        {
            _financeManagerContext = financeManagerContext;
        }

        public Outgoing GetOutGoing(long id, string userId)
        {
            return _financeManagerContext.Outgoings.SingleOrDefault(x => x.Id == id && x.UserId.Equals(userId));
        }

        public bool RemoveOutGoing(long id, string userId)
        {
            bool success;
            try
            {
                _financeManagerContext.Outgoings.Remove(GetOutGoing(id, userId));
                _financeManagerContext.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        public Outgoing UpdateOutgoing(Outgoing outgoing)
        {
            Outgoing tempOutgoing;
            try
            {
                tempOutgoing = _financeManagerContext.Outgoings.Attach(outgoing);
                _financeManagerContext.Entry(tempOutgoing).State = EntityState.Modified;
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                tempOutgoing = null;
            }
            return tempOutgoing;
        }

        public Outgoing AddOutgoing(Outgoing outgoing)
        {
            Outgoing tempOutgoing;
            try
            {
                tempOutgoing = _financeManagerContext.Outgoings.Add(outgoing);
                _financeManagerContext.SaveChanges();
            }
            catch (Exception ex)
            {
                tempOutgoing = null;
            }
            return tempOutgoing;
        }

        public double SumOfOutgoings(string userId)
        {
            return GetOutGoings(userId).Sum(x => x.Amount);
        }

        public double SumOfOutgoings(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return GetOutGoings(firstDateTime, secondDateTime, userId).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfDays(int days, string userId)
        {
            return GetOutgoingsByNumberOfDays(days, userId).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfWeeks(int weeks, string userId)
        {
            return GetOutgoingsByNumberOfWeeks(weeks, userId).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfMonth(int month, string userId)
        {
            return GetOutgoingsByNumberOfMonth(month, userId).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByLastOperations(int count, string userId)
        {
            return GetOutgoingsByLastOperations(count, userId).Sum(x => x.Amount);
        }

        public IEnumerable<Outgoing> GetOutGoings(DateTime firstDateTime, DateTime secondDateTime, string userId)
        {
            return _financeManagerContext.Outgoings.Where(x => TruncateTime(x.Date) >= TruncateTime(firstDateTime.Date) && TruncateTime(x.Date) <= TruncateTime(secondDateTime.Date) && x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfDays(int days, string userId)
        {
            var daysAgo = DateTime.Now.AddDays(days * -1);

            return GetOutGoings(daysAgo, DateTime.Now, userId);
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfWeeks(int weeks, string userId)
        {
            var weeksAgo = DateTime.Now.AddDays((weeks * 7) * -1);

            return GetOutGoings(weeksAgo, DateTime.Now, userId);
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfMonth(int month, string userId)
        {
            var monthsAgo = DateTime.Now.AddMonths(month * -1);

            return GetOutGoings(monthsAgo, DateTime.Now, userId);
        }

        public IEnumerable<Outgoing> GetOutGoings(string userId)
        {
            return _financeManagerContext.Outgoings.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Outgoing> GetOutgoingsByLastOperations(int count, string userId)
        {
            return _financeManagerContext.Outgoings.Where(x => x.UserId.Equals(userId)).Take(count).ToList();
        }
    }
}