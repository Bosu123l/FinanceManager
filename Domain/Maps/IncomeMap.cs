using Domain.Models;
using FluentNHibernate.Mapping;

namespace Domain.Maps
{
    internal class IncomeMap : ClassMap<Income>
    {
        public IncomeMap()
        {
            Table("Income");

            Id(x => x.ID).Column("ID").GeneratedBy.Increment();
            Map(x => x.Amount).Column("Amount");
            Map(x => x.Date).Column("TimeSpan");
            References(x => x.Source).Columns("Source").ReadOnly();
            Map(x => x.SourceID).Column("Source");
            Map(x => x.Description).Column("Description");

        }
    }
}