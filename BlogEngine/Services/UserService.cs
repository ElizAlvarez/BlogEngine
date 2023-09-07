using System;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;

namespace BlogEngine.Services
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
        }

        public User? GetUser(UserAuth user)
        {
            var result = _userRepository.GetUser(user);
            return result;
        }
    }
}

