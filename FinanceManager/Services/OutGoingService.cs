using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
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

    public class OutGoingService : IOutGoingService
    {
        private readonly OutgoingRepository _outgoingRepository;

        public OutGoingService(OutgoingRepository outgoingRepository)
        {
            _outgoingRepository = outgoingRepository;
        }

        public Outgoing GetOutGoing(long id)
        {
            return _outgoingRepository.GetById(id);
        }

        public bool RemoveOutGoing(long id)
        {
            bool success;
            try
            {
                success = _outgoingRepository.Delete(this.GetOutGoing(id));
                _outgoingRepository.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public Outgoing UpdateOutgoing(Outgoing outgoing)
        {
            Outgoing tempOutgoing;
            try
            {
                tempOutgoing = _outgoingRepository.Update(outgoing);
                _outgoingRepository.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempOutgoing;
        }

        public Outgoing AddOutgoing(Outgoing outgoing)
        {
            Outgoing tempOutgoing;
            try
            {
                tempOutgoing = _outgoingRepository.Add(outgoing);
                _outgoingRepository.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tempOutgoing;
        }

        public double SumOfOutgoings()
        {
            return GetOutGoings().Sum(x => x.Amount);
        }

        public double SumOfOutgoings(DateTime firstDateTime, DateTime secondDateTime)
        {
            return GetOutGoings(firstDateTime, secondDateTime).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfDays(int days)
        {
            return GetOutgoingsByNumberOfDays(days).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfWeeks(int weeks)
        {
            return GetOutgoingsByNumberOfWeeks(weeks).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByNumberOfMonth(int month)
        {
            return GetOutgoingsByNumberOfMonth(month).Sum(x => x.Amount);
        }

        public double SumOfOutgoingsByLastOperations(int count)
        {
            return GetOutgoingsByLastOperations(count).Sum(x => x.Amount);
        }

        public IEnumerable<Outgoing> GetOutGoings(DateTime firstDateTime, DateTime secondDateTime)
        {
            return _outgoingRepository.FilterBy(x => x.Date.Value.Date >= firstDateTime.Date && x.Date.Value.Date <= secondDateTime.Date).ToList();
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfDays(int days)
        {
            var daysAgo = DateTime.Now.AddDays(days * -1);

            return GetOutGoings(daysAgo, DateTime.Now);
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfWeeks(int weeks)
        {
            var weeksAgo = DateTime.Now.AddDays((weeks * 7) * -1);

            return GetOutGoings(weeksAgo, DateTime.Now);
        }

        public IEnumerable<Outgoing> GetOutgoingsByNumberOfMonth(int month)
        {
            var monthsAgo = DateTime.Now.AddMonths(month * -1);

            return GetOutGoings(monthsAgo, DateTime.Now);
        }

        public IEnumerable<Outgoing> GetOutGoings()
        {
            return _outgoingRepository.All().ToList();
        }

        public IEnumerable<Outgoing> GetOutgoingsByLastOperations(int count)
        {
            return _outgoingRepository.All().Take(count).ToList();
        }
    }
}