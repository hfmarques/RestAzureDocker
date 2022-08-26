using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository.Generic;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext context;

    public Repository(DbContext context)
    {
        this.context = context;
    }

    public T? Get(long id)
    {
        return context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Where(predicate);
    }

    public T Add(T entity)
    {
        context.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public void Update(T entity)
    {
        context.Update(entity);
        context.SaveChanges();
    }

    public void Remove(T entity)
    {
        context.Remove(entity);
        context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        context.RemoveRange(entities);
        context.SaveChanges();
    }
}