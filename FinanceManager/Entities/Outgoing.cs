using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Entities
{
    public class Outgoing
    {
        [Key]
        [Index(IsUnique = true)]
        public long Id { get; set; }

        public double Amount { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual TypeOfOutgoing Type { get; set; }

        public long TypeId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}