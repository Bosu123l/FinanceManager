using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManager.Services
{
    public class OutGoingService
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

        public IEnumerable<Outgoing> GetOutGoings(DateTime firstDateTime, DateTime secondDateTime)
        {
            return _outgoingRepository.FilterBy(x => x.Date.Value.Date >= secondDateTime.Date && x.Date.Value.Date <= secondDateTime.Date).ToList();
        }

        public IEnumerable<Outgoing> GetOutGoings()
        {
            return _outgoingRepository.All().ToList();
        }
    }
}