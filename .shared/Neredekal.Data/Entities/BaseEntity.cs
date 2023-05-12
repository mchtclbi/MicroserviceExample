using System;

namespace Neredekal.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public Guid? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}