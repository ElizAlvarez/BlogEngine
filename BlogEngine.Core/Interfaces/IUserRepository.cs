using System;
using BlogEngine.Core.Entities;

namespace BlogEngine.Core.Interfaces
{
	public interface IUserRepository
	{
        public User? GetUser(UserAuth user);

    }
}

