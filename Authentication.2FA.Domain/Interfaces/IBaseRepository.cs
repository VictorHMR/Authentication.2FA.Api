using Authentication._2FA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Domain.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task<T> Create(T entity);

        Task<T> GetByID(int id);
    }
}
