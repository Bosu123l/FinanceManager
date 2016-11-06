namespace Domain.Models
{
    public class Outgoing
    {
        public virtual long ID { get; set; }
        public virtual double Amount { get; set; }
        public virtual TypeOfOutgoing Type { get; set; }
    }
}