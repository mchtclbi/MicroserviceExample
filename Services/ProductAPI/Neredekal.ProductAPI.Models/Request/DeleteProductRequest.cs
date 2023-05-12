using System;
using Neredekal.Application.Models.Request;

namespace Neredekal.ProductAPI.Models.Request
{
    public class DeleteProductRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}