using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
    public interface IUserService
    {
        User RetrieveUser(string id);
        IEnumerable<User> GetAllUsers();
        User PromoteUser(string id);
        User LockUser(string id);
        User EditUser(User user);
    }
}
