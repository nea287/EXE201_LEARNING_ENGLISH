using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_Repository.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task InsertAsync(TEntity entity);
        public Task InsertRangeAsync(IQueryable<TEntity> entities);
        public DbSet<TEntity> GetAll();
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                            string? includeProperties = null);
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null
                                    , string? includeProperties = null);
        public IQueryable<TEntity> GetAllApart();
        public Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);
        public IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate);
        public TEntity Find(Func<TEntity, bool> predicate);
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity> GetByIdByInt(int id);
        public Task<TEntity> GetByIdByString(string id);
        public Task<TEntity> GetByIdGuid(Guid id);
        public void Insert(TEntity entity);
        public Task UpdateByIdByString(TEntity entity, string id);
        public Task UpdateById(TEntity entity, int id);
        public Task UpdateGuid(TEntity entity, Guid id);
        public void UpdateRange(IQueryable<TEntity> entities);
        public Task HardDeleteByString(string key);
        public Task HardDelete(int key);
        public Task HardDeleteGuid(Guid key);
        public void DeleteRange(IQueryable<TEntity> entities);
        public void InsertRange(IQueryable<TEntity> entities);
        public Task Update(TEntity entity);
        public void Save();
    }
}
