using BlogEngine.Core.Entities;
using BlogEngine.Core.Enumerations;
using BlogEngine.Core.Interfaces;
using BlogEngine.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Repositories
{
    public class PostRepository : IPostRepository
	{
		private readonly BlogEngineDbContext _context;

		public PostRepository(BlogEngineDbContext context)
		{
			_context = context;
		}

		public async Task<Post> Create(Post post)
		{
			_context.Posts.Add(post);
			await _context.SaveChangesAsync();
			return post;
		}

        public async Task<Post>? Edit(Post post)
        {
			var result = _context.Posts.FirstOrDefault(x => x.PostId == post.PostId);

			if (result == null)
				return null;

            result.Title = post.Title;
			result.TextContent = post.TextContent;
			await _context.SaveChangesAsync();
			return result;
        }

        public async Task<List<Post>> GetAllByUser(int userId)
		{
			var posts = _context.Posts.Where(x => x.UserId == userId &&
				x.PostStatusId == (int)PostStatusEnum.Created);

			var listPosts = await posts.ToListAsync();
			return listPosts;
		}

		public async Task<List<Post>> GetAllPendingForApproval()
		{
			var posts = _context.Posts.Where(x => x.PostStatusId == (int)PostStatusEnum.Pending);

            var listPosts = await posts.ToListAsync();
            return listPosts;
        }

        public async Task<List<Post>> GetAllPublished()
        {
            var posts = _context.Posts.Where(x => x.PostStatusId == (int)PostStatusEnum.Published);

            var listPosts = await posts.ToListAsync();
            return listPosts;
        }

        public async Task<bool> Submit(int postId)
        {
            var result = _context.Posts.FirstOrDefault(x => x.PostId == postId);

            if (result == null)
                return false;

			result.PostStatusId = (int)PostStatusEnum.Pending;
			await _context.SaveChangesAsync();
			return true;
        }
    }
}

