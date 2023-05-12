using Neredekal.Data.Entities;

namespace Neredekal.ProductAPI.Models.Entities
{
    public class Product : BaseEntity
    {
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }

        public string CompanyTitle { get; set; }
    }   
}