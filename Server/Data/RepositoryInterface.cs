using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace Data;

public interface IRepository<T> where T : class {
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
}
