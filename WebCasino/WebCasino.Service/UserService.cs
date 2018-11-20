using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;

namespace WebCasino.Service
{
    public class UserService : IUserService
    {
        private readonly CasinoContext context;
        public UserService(CasinoContext context)
        {
            this.context = context;
        }
        public User EditUser(User user)
        {

        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User LockUser(string id)
        {
            throw new NotImplementedException();
        }

        public User PromoteUser(string id)
        {
            throw new NotImplementedException();
        }

        public User RetrieveUser(string id)
        {
            var user = this.context.Users.FirstOrDefault(us => us.Id == id && !us.Locked);
            if(user == null)
            {

            }
            return user;
        }
    }
}
