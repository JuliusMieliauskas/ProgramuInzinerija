using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data;

public interface IRepository<T> where T : class {
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
}
