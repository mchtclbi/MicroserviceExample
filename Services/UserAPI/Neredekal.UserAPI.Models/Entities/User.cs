using Neredekal.Data.Entities;

namespace Neredekal.UserAPI.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}