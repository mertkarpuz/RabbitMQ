using Domain;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository(MsSqlDbContext context) : IUserRepository
    {
        private readonly MsSqlDbContext context = context;

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
