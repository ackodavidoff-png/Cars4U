using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Repository.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Repository.Common
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = dbContext.Set<TEntity>();
        }
        protected ApplicationDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }
        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.AddRange(entityCollection);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            await DbSet.AddRangeAsync(entityCollection);
        }

        public bool Contains(TEntity entity)
        {
            return DbSet.Contains(entity);
        }

        public async Task<bool> ContainsAsync(TEntity entity)
        {
            return await DbSet.ContainsAsync(entity);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return DbSet.AsNoTracking().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.RemoveRange(entityCollection);
        }

        public int SaveChanges()
        {
            int result = DbContext.SaveChanges();
            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await DbContext.SaveChangesAsync();
            return result;
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.UpdateRange(entityCollection);
        }
    }
}
