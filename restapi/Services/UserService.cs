using System.Collections.Generic;
using System.Linq;
using restapi.Models;

namespace restapi.Services
{
    public class UserService
    {
        private UserContext context;

        public UserService(UserContext context)
        {
            this.context = context;
        }

        public void Add(int id, string name)
        {
            var newUser = new User(id, name);
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public IEnumerable<User> All()
        {
            return context.Users.ToList();
        }
    }
}
