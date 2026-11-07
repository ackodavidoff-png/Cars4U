using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Data.Repository.Contracts.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsNoTracking();
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entityCollection);
        Task AddRangeAsync(IEnumerable<TEntity> entityCollection);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entityCollection);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entityCollection);
        bool Contains(TEntity entity);
        Task<bool> ContainsAsync(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
