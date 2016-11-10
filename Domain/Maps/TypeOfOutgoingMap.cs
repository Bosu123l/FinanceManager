﻿using Domain.Models;
using FluentNHibernate.Mapping;

namespace Domain.Maps
{
    internal class TypeOfOutgoingMap : ClassMap<TypeOfOutgoing>
    {
        public TypeOfOutgoingMap()
        {
            Table("TypeOfOutgoing");

            Id(x => x.ID).Column("ID").GeneratedBy.Increment();

            Map(x => x.Name).Column("Name");
        }
    }
}