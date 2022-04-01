
using Domain.Entities;

namespace Infra.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Base 
    {
        Task<T> GetById(long id);
        Task<List<T>> GetAll();
        Task<T> Create(T obj);
        Task<T> Updade(T obj);
        Task Remove(long id);
    }
}