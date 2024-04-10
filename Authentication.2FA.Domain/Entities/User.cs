using Authentication._2FA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Domain.Entities
{
    public sealed class User: BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime? LastValidation { get; private set; }

        public User(int id, string name, string email, string password, DateTime dateCreated, DateTime? lastValidation, DateTime? dateUpdated, DateTime? dateDeleted)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.DateCreated = dateCreated;
            this.LastValidation = lastValidation;
            this.DateUpdated = dateUpdated;
            this.DateDeleted = dateDeleted;
        }
        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = HashMD5.HasPassword(password);
        }

        public void SetUserIDCreated(int id, DateTime dateCreated)
        {
            this.Id = id;
            this.DateCreated = dateCreated;
        }
    }
}
