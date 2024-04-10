using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Infrastructure.Context;
using Authentication._2FA.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Authentication_2FAContext dbAuth2FA;
        public UserRepository(Authentication_2FAContext dbAuth2FA)
        {
            this.dbAuth2FA = dbAuth2FA;
        }

        public async Task<User> Create(User request)
        {
            try
            {
                if (dbAuth2FA.USERs.Where(x => x.DS_EMAIL == request.Email).Any())
                    throw new Exception("O email informado já possui um cadastro!");
                USER dbUser = new USER()
                {
                    DS_EMAIL = request.Email,
                    DS_NAME = request.Name,
                    DS_PASSWORD = request.Password,
                    CREATED_AT = DateTime.Now
                };
                await dbAuth2FA.USERs.AddAsync(dbUser);
                await dbAuth2FA.SaveChangesAsync();
                request.SetUserIDCreated(dbUser.ID_USER, dbUser.CREATED_AT);
                return request;
            }
            catch (Exception ex) { throw; }
        }

        public async Task<User> GetByID(int Id)
        {
            try
            {
                var dbUser = await dbAuth2FA.USERs.FindAsync(Id);
                return new User(dbUser.ID_USER, dbUser.DS_NAME, dbUser.DS_EMAIL, dbUser.DS_PASSWORD, dbUser.CREATED_AT, dbUser.LAST_VALIDATION, dbUser.UPDATED_AT, dbUser.DELETED_AT);
            }
            catch (Exception ex) { throw; }
        }

        public async Task<User> GetUserByEmailPassword(string email, string password)
        {
            try
            {
                var dbUser = await dbAuth2FA.USERs.Where(x => x.DS_EMAIL == email && x.DS_PASSWORD == password).FirstOrDefaultAsync();
                if (dbUser is null)
                    return null;
                return new User(dbUser.ID_USER, dbUser.DS_NAME, dbUser.DS_EMAIL, dbUser.DS_PASSWORD, dbUser.CREATED_AT, dbUser.LAST_VALIDATION, dbUser.UPDATED_AT, dbUser.DELETED_AT);

            }
            catch (Exception ex) { throw; }
        }

        public async Task SetLastValidation(int Id)
        {
            try
            {
                var dbUser = await dbAuth2FA.USERs.FindAsync(Id);
                dbUser.LAST_VALIDATION = DateTime.Now;
                await dbAuth2FA.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }
    }
}
