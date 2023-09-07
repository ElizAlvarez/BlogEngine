using System;
using BlogEngine.Core.Entities;

namespace BlogEngine.Core.Interfaces
{
	public interface IPostRepository
	{
        public Task<Post> Create(Post post);
        public Task<List<Post>> GetAllByUser(int userId);
        public Task<List<Post>> GetAllPendingForApproval();
        public Task<List<Post>> GetAllPublished();
        public Task<bool> Submit(int postId);
        public Task<Post>? Edit(Post post);
    }
}

