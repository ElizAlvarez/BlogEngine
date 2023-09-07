using System;
using BlogEngine.Core.Entities;

namespace BlogEngine.Core.Interfaces
{
	public interface IUserService
	{
        public User? GetUser(UserAuth user);
    }
}

