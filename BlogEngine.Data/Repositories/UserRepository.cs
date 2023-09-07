using System;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using BlogEngine.Data.Context;

namespace BlogEngine.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly BlogEngineDbContext _context;

        public UserRepository(BlogEngineDbContext context)
		{
            _context = context;
        }

        public User? GetUser(UserAuth user)
        {
            var result = _context.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName)
                && x.Email.Equals(user.Email));

            return result;
        }
	}
}

