using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain
{
    public interface IBaseEntity<Tid>
    {
        public Tid Id { get; set; }
    }
}