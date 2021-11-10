using Conventions.Models.Entities;
using System;
using System.Collections.Generic;

namespace Conventions.API.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        void CreateUser(User person);
        void UpdateUser(User person);
        void DeleteUser(Guid id);
    }
}
