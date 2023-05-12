using System;

namespace Neredekal.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
    }
}