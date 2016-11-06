namespace Domain.Models
{
    public class Income
    {
        public virtual long ID { get; set; }
        public virtual double Amount { get; set; }
        public virtual SourceOfAmount Source { get; set; }
    }
}