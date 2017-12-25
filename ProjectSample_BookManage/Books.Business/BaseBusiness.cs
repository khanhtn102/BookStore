using Books.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business
{
    public abstract class BaseBusiness<T> : IBaseBusiness<T> where T : class
    {
        IUnitOfWork unitOfWork;
        IBaseRepository<T> repository;

        public BaseBusiness(IUnitOfWork uniOfWork, IBaseRepository<T> repository)
        {
            this.unitOfWork = uniOfWork;
            this.repository = repository;
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Insert(entity);
            unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Delete(entity);
            unitOfWork.Commit();
        }

        public IEnumerable<T> GetAll()
        {
            return repository.SelectAll();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Update(entity);
            unitOfWork.Commit();
        }
    }
}
