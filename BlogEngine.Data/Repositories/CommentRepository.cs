using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using BlogEngine.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Repositories
{
    public class CommentRepository : ICommentRepository
	{
		private readonly BlogEngineDbContext _context;

		public CommentRepository(BlogEngineDbContext context)
		{
			_context = context;
		}

		public async Task<Comment> Create(Comment comment)
		{
			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();
			return comment;
		}

		public async Task<List<Comment>> GetAllByPost(int postId)
		{
			var comments = _context.Comments.Where(x => x.PostId == postId);
			var listComments = await comments.ToListAsync();
			return listComments;
		}
	}
}

