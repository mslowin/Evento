using Evento.Evento.Core.Domain;
using Evento.Evento.Core.Repositories;
using System.Data;
using System.Xml.Linq;

namespace Evento.Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(Guid userid, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            user = new User(userid, role, name, email, password);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }
            if(user.Password != password)
            {
                throw new Exception($"Invalid credentials.");
            }

        }
    }
}
