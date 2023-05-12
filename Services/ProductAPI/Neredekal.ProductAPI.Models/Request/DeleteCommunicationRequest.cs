using System;
using Neredekal.Application.Models.Request;

namespace Neredekal.ProductAPI.Models.Request
{
    public class DeleteCommunicationRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
        public Guid CommunicationId { get; set; }
    }
}