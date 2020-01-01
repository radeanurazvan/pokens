﻿using MongoDB.Bson.Serialization;
using Pomelo.Kernel.Domain;

namespace Pokens.Training.Infrastructure
{
    internal sealed class AggregateMap : BsonClassMap<DocumentAggregate>
    {
        public AggregateMap()
        {
            UnmapProperty(a => a.Events);
        }
    }
}