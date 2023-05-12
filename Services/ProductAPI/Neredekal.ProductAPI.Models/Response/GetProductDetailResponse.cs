using System.Collections.Generic;

namespace Neredekal.ProductAPI.Models.Response
{
    public class GetProductDetailResponse
    {
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }
        public string CompanyTitle { get; set; }
        public List<CommunicationViewModel> Communications { get; set; }
    }

    public class CommunicationViewModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}