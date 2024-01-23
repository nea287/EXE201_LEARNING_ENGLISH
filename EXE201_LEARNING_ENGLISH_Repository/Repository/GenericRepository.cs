using EXE201_LEARNING_ENGLISH_DAO.DAO;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_Repository.Repository
{
    public class GenericRepository<T> : GenericDAO<T>, IGenericRepository<T> where T : class
    {
        public GenericRepository(EXE201_LEARNING_ENGLISHContext context) : base(context)
        {
        }
        public override Task InsertAsync(T entity)
        {
            return base.InsertAsync(entity);


        }
        public override async Task InsertRangeAsync(IQueryable<T> entities)
        {
            try
            {
                base.InsertRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public override DbSet<T> GetAll()
        {
            DbSet<T> db;
            try
            {
                db = base.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return db;
        }
        public override IQueryable<T> GetAllApart()
        {
            IQueryable<T> db;
            try
            {
                db = base.GetAllApart();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return db;
        }
        public override Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            Task<IEnumerable<T>> db;
            try
            {
                db = base.GetWhere(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return db;

        }
        public override IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            IQueryable<T> rs;
            try
            {
                rs = base.FindAll(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public override T Find(Func<T, bool> predicate)
        {
            T result;
            try
            {
                result = base.Find(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public override Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            Task<T> result;
            try
            {
                result = base.FindAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public override Task<T> GetByIdByInt(int id)
        {
            Task<T> result;
            try
            {
                result = base.GetByIdByInt(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public override Task<T> GetByIdByString(string id)
        {
            Task<T> result;
            try
            {
                result = base.GetByIdByString(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public override Task<T> GetByIdGuid(Guid id)
        {
            Task<T> result;
            try
            {
                result = base.GetByIdGuid(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public override void Insert(T entity)
        {
            try
            {
                base.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task UpdateByIdByString(T entity, string id)
        {
            try
            {
                base.UpdateByIdByString(entity, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task UpdateById(T entity, int id)
        {
            try
            {
                base.UpdateById(entity, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task UpdateGuid(T entity, Guid id)
        {
            try
            {
                base.UpdateGuid(entity, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override void UpdateRange(IQueryable<T> entities)
        {
            try
            {
                base.UpdateRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task HardDeleteByString(string key)
        {
            try
            {
                base.HardDeleteByString(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task HardDelete(int key)
        {
            try
            {
                base.HardDelete(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task HardDeleteGuid(Guid key)
        {
            try
            {
                base.HardDeleteGuid(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override void DeleteRange(IQueryable<T> entities)
        {
            try
            {
                base.DeleteRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override void InsertRange(IQueryable<T> entities)
        {
            try
            {
                base.InsertRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override async Task Update(T entity)
        {
            try
            {
                base.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Any(Expression<Func<T, bool>> predicate)
        {
            bool result = false;
            try
            {
                result = base.Any(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public override void Save()
        {
            try
            {
                base.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IEnumerable<T> result;
            try
            {
                result = base.GetAll(filter, orderBy, includeProperties);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            T result;
            try
            {
                result = base.getFirstOrDefault(filter, includeProperties);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
