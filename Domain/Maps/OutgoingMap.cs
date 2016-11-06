using Domain.Models;
using FluentNHibernate.Mapping;

namespace Domain.Maps
{
    internal class OutgoingMap : ClassMap<Outgoing>
    {
        public OutgoingMap()
        {
            Table("Outgoing");

            Id(x => x.ID).Column("ID");

            Map(x => x.Amount).Column("Amount");
            References(x => x.Type).Column("Type");
        }
    }
}