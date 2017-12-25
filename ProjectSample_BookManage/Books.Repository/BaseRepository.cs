using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<T> SelectAll()
        {
            return dbSet.ToList();
        }

        public T SelectById(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            dbContext.Entry(obj).State = EntityState.Modified;
        }
    }
}
