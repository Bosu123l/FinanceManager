using System;

namespace Domain.Models
{
    public class Outgoing
    {
        public virtual long ID { get; set; }
        public virtual double Amount { get; set; }
        public virtual TypeOfOutgoing Type { get; set; }
        public virtual long TypeID { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string Description { get; set; }
    }
}