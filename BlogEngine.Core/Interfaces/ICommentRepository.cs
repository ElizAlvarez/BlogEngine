using System;
using BlogEngine.Core.Entities;

namespace BlogEngine.Core.Interfaces
{
	public interface ICommentRepository
	{
        Task<Comment> Create(Comment comment);
        Task<List<Comment>> GetAllByPost(int postId);
    }
}

