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
            References(x => x.Type).Column("Type").ReadOnly();
            Map(x => x.TypeID).Column("Type");
            Map(x => x.Description).Column("Description");
            Map(x => x.Date).Column("TimeSpan");
        }
    }
}