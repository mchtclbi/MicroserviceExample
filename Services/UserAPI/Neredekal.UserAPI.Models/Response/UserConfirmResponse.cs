using System;

namespace Neredekal.UserAPI.Models.Response
{
    public class UserConfirmResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        
        public string FullName { get { return $"{Name} {Surname}"; } }
    }
}