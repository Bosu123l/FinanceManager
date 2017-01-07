using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Entities
{
    public class Income
    {
        [Key]
        [Index(IsUnique = true)]
        public long Id { get; set; }

        public double Amount { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey(nameof(SourceId))]
        public virtual SourceOfAmount Source { get; set; }

        public long SourceId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}