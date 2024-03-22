using Application.Interfaces;
using Domain;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        public void AddUser(User user)
        {
            userRepository.AddUser(user);
        }
    }
}
