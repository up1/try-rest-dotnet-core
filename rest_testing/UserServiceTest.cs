using System;
using Xunit;

using restapi.Services;
using restapi.Models;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace rest_testing.Services
{
    public class UserServiceTest
    {
        [Fact]
        public void Add_new_user_success_with_in_memory_database() {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: "try_to_test")
                .Options;

            var context = new UserContext(options);
            var service = new UserService(context);
            service.Add(1, "User 1");
            service.Add(2, "User 2");

            Assert.Equal(2, context.Users.Count());
        }

        [Fact]
        public void search_all_users_from_inmemory_database() {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: "try_to_search")
                .Options;

            using (var context = new UserContext(options))
            {
                context.Users.Add(new User(1, "User 1"));
                context.Users.Add(new User(2, "User 2"));
                context.SaveChanges();
            }

            using (var context = new UserContext(options))
            {
                var service = new UserService(context);
                var results = service.All();
                Assert.Equal(2, results.Count());
            }
        }
    }
}
