using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Entities
{
    public class SourceOfAmount
    {
        [Key]
        [Index(IsUnique = true)]
        public long Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
    }
}