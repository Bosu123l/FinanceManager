using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Income
    {
        public virtual long ID { get; set; }
        public virtual double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public virtual DateTime? Date { get; set; }
        public virtual SourceOfAmount Source { get; set; }
        public virtual long SourceID { get; set; }
        public virtual string Description { get; set; }
    }
}