using System;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;

namespace BlogEngine.Services
{
	public class CommentService : ICommentService
	{
		private ICommentRepository _commentRepository;

		public CommentService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

        public async Task<Comment> Create(Comment comment)
        {
            var response = await _commentRepository.Create(comment);
            return response;
        }

        public async Task<List<Comment>> GetAllByPost(int postId)
        {
            var response = await _commentRepository.GetAllByPost(postId);
            return response;
        }
    }
}

