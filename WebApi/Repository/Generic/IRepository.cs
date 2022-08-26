using System.Linq.Expressions;

namespace WebApi.Repository.Generic;

public interface IRepository<T> where T : class
{
    T? Get(long id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    T Add(T entity);
    void Update(T entity);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}