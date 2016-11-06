using Domain.Models;
using FluentNHibernate.Mapping;

namespace Domain.Maps
{
    internal class IncomeMap : ClassMap<Income>
    {
        public IncomeMap()
        {
            Table("Income");

            Id(x => x.ID).Column("ID");
            Map(x => x.Amount).Column("Amount");
            References(x => x.Source).Columns("Source");
        }
    }
}