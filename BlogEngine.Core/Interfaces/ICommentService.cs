using System;
using BlogEngine.Core.Entities;

namespace BlogEngine.Core.Interfaces
{
	public interface ICommentService
	{
        public Task<Comment> Create(Comment comment);
        public Task<List<Comment>> GetAllByPost(int postId);
    }
}

