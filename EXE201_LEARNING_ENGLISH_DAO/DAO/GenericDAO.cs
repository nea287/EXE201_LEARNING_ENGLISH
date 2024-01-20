using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_DAO.DAO
{
    public class GenericDAO<T> where T : class
    {
        private readonly EXE201_LEARNING_ENGLISHContext Context;

        private DbSet<T> Table { get; set; }

        public GenericDAO(EXE201_LEARNING_ENGLISHContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }
        public virtual void DeleteRange(IQueryable<T> entities)
        {
            Table.RemoveRange(entities);
        }

        public virtual T Find(Func<T, bool> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.SingleOrDefaultAsync(predicate);
        }

        public virtual DbSet<T> GetAll()
        {
            return Table;
        }

        public virtual IQueryable<T> GetAllApart()
        {
            return Table.Take(100);
        }

        public virtual async Task<T> GetByIdByInt(int id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task<T> GetByIdByString(string id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task<T> GetByIdGuid(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public virtual async Task HardDeleteByString(string key)
        {
            var rs = await GetByIdByString(key);
            Table.Remove(rs);
        }
        public virtual async Task HardDelete(int key)
        {
            var rs = await GetByIdByInt(key);
            Table.Remove(rs);
        }


        public virtual async Task HardDeleteGuid(Guid key)
        {
            var rs = await GetByIdGuid(key);
            Table.Remove(rs);
        }

        public virtual void Insert(T entity)
        {
            Table.Add(entity);
        }

        public virtual async Task InsertAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public virtual void InsertRange(IQueryable<T> entities)
        {
            Table.AddRange(entities);
        }

        public virtual async Task InsertRangeAsync(IQueryable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public virtual async Task UpdateByIdByString(T entity, string id)
        {
            var existEntity = await GetByIdByString(id);
            Context.Entry(existEntity).CurrentValues.SetValues(entity);
            Table.Update(existEntity);
        }
        public virtual async Task UpdateById(T entity, int id)
        {
            var existEntity = await GetByIdByInt(id);
            Context.Entry(existEntity).CurrentValues.SetValues(entity);
            Table.Update(existEntity);
        }

        public virtual async Task UpdateGuid(T entity, Guid id)
        {
            var existEntity = await GetByIdGuid(id);
            Context.Entry(existEntity).CurrentValues.SetValues(entity);
            Table.Update(existEntity);
        }

        public virtual void UpdateRange(IQueryable<T> entities)
        {
            Table.RemoveRange(entities);
        }

        public virtual async Task Update(T entity)
        {
            Table.Update(entity);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null)
        {
            IQueryable<T> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public virtual T getFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }


        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
