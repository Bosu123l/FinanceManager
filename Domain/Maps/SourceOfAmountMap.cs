using Domain.Models;
using FluentNHibernate.Mapping;

namespace Domain.Maps
{
    internal class SourceOfAmountMap : ClassMap<SourceOfAmount>
    {
        public SourceOfAmountMap()
        {
            Table("SourceOfAmount");

            Id(x => x.ID).Column("ID");
            Map(x => x.Name).Column("Name");
        }
    }
}