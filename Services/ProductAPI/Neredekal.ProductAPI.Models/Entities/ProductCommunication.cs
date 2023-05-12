using System;
using Neredekal.Data.Entities;

namespace Neredekal.ProductAPI.Models.Entities
{
    public class ProductCommunication : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid TypeId { get; set; }
        public object Value { get; set; }
    }
}