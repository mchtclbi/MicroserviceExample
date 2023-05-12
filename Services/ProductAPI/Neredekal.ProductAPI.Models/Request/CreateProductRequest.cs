using System.Collections.Generic;
using Neredekal.Application.Models.Request;

namespace Neredekal.ProductAPI.Models.Request
{
    public class CreateProductRequest : BaseRequest
    {
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }
        public string CompanyTitle { get; set; }
        public List<CreateCommuncationModel> Communcations { get; set; }
    }
}