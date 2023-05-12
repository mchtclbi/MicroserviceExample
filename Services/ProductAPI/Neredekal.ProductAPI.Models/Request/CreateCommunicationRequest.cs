using System;
using System.Collections.Generic;
using Neredekal.Application.Models.Request;

namespace Neredekal.ProductAPI.Models.Request
{
    public class CreateCommunicationRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
        public List<CreateCommuncationModel> Communications { get; set; }
    }
}