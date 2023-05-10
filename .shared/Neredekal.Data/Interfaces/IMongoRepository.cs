using Neredekal.Data.Entities;

namespace Neredekal.Data.Interfaces
{
    public interface IMongoRepository<T> : IRepository<T> where T : BaseEntity, new()
    {

    }
}