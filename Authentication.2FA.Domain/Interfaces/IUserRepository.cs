using Authentication._2FA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Domain.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetUserByEmailPassword(string email, string password);
        Task SetLastValidation(int Id);
    }
}
