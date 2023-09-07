using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;

namespace BlogEngine.Services
{
	public class PostService : IPostService
	{
		private IPostRepository _postRepository;

		public PostService(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}

		public async Task<Post> Create(Post post)
		{
			var response = await _postRepository.Create(post);
			return response;
		}

		public async Task<List<Post>> GetAllPublished()
		{
			var posts = await _postRepository.GetAllPublished();
			return posts;
		}

		public async Task<List<Post>> GetAllByUser(int userId)
		{
			var posts = await _postRepository.GetAllByUser(userId);
			return posts;
		}

		public async Task<List<Post>> GetAllPendingForApproval()
		{
			var posts = await _postRepository.GetAllPendingForApproval();
			return posts;
		}

        public async Task<bool> Submit(int postId)
        {
			var submitted = await _postRepository.Submit(postId);
			return submitted;
        }

        public async Task<Post>? Edit(Post post)
        {
			var result = await _postRepository.Edit(post);
			return result;
        }
    }
}

