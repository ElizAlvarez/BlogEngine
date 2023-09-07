using System;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using BlogEngine.Services;
using Moq;

namespace BlogEngine.UnitTests.Services
{
	public class PostServiceUnitTest
	{
		Mock<IPostRepository> _mockRepository;
		public PostServiceUnitTest()
		{
			_mockRepository = new Mock<IPostRepository>();
		}

		[Fact]
		public void CreatePost_ValidPost_ReturnPost()
		{
			var post = new Post()
			{
				PostId = 5,
				Title = "UnitTest title",
				TextContent = "UnitTest content",
				PostStatusId = 1,
				UserId = 1,
				CreationDate = DateTime.Now
			};
			_mockRepository.Setup(x => x.Create(post)).ReturnsAsync(post);

			var service = new PostService(_mockRepository.Object);
			var result = service.Create(post).Result;

			Assert.NotNull(result);
			Assert.True(result.PostId != 0);
		}

        [Fact]
        public void CreatePost_InvalidPost_ReturnNull()
        {
            var post = new Post()
            {
                CreationDate = DateTime.Now
            };
			_mockRepository.Setup(x => x.Create(post)).Returns(Task.FromResult<Post>(null!));

            var service = new PostService(_mockRepository.Object);
            var result = service.Create(post).Result;

            Assert.Null(result);
        }

        [Fact]
        public void GetAllPublished_ExistsPost_ReturnPosts()
        {
			var posts = new List<Post>()
			{
                new Post() { PostId = 5, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 1, UserId = 1, CreationDate = DateTime.Now },
				new Post() { PostId = 7, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 2, UserId = 3, CreationDate = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetAllPublished()).ReturnsAsync(posts);

            var service = new PostService(_mockRepository.Object);
            var result = service.GetAllPublished().Result;

            Assert.NotNull(result);
			Assert.Collection<Post>(result, x => Assert.Equal(5, x.PostId), x => Assert.Equal(7, x.PostId));
        }

        [Fact]
        public void GetAllPublished_NotExistPost_ReturnNull()
        {
            var posts = new List<Post>() { };
            _mockRepository.Setup(x => x.GetAllPublished()).Returns(Task.FromResult<List<Post>>(null!));

            var service = new PostService(_mockRepository.Object);
            var result = service.GetAllPublished().Result;

            Assert.Null(result);
        }
    }
}

