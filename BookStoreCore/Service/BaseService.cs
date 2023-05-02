using BookStoreCore.IRepository;
using BookStoreCore.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreInfrastructure.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public IBaseRepository<T> _IRepository;
        public BaseService(IBaseRepository<T> baseRepository) { 
            _IRepository= baseRepository;   
        }
        public virtual void Insert(T entity)
        {
            // validate T
            _IRepository.Insert(entity);

        }

        public void Update(T entity)
        {
            _IRepository.Update(entity);
        }
    }
}
