using System;
using BlogEngine.Controllers;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogEngine.UnitTests.Controllers
{
	public class PostControllerUnitTest
	{
		private Mock<IPostService> _mockPostService;
		private PostController _postController;

		public PostControllerUnitTest()
		{
			_mockPostService = new Mock<IPostService>();
			_postController = new PostController(_mockPostService.Object);
		}

		[Fact]
		public void Create_ValidPost_Return200()
		{
			var post = new Post()
			{
				PostId = 3,
				PostStatusId = 1,
				Title = "UnitTest title",
				TextContent = "UnitTest content",
				UserId = 1,
				CreationDate = DateTime.Now
			};

			var result = _postController.Create(post).Result;
			var okResult = result as OkObjectResult;

			Assert.NotNull(result);
			Assert.Equal(200, okResult?.StatusCode);
		}

        [Fact]
        public void Create_InvalidRequest_Return400()
        {
            var post = new Post()
            {
                Title = "UnitTest title"
            };
			_mockPostService.Setup(x => x.Create(post)).Throws<Exception>();

            var result = _postController.Create(post).Result;
            var badResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, badResult?.StatusCode);
        }

        [Fact]
        public void GetAllPublished_ExistsPosts_Return200()
        {
            var posts = new List<Post>()
            {
                new Post() { PostId = 5, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 1, UserId = 1, CreationDate = DateTime.Now },
                new Post() { PostId = 7, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 2, UserId = 3, CreationDate = DateTime.Now }
            };
            _mockPostService.Setup(x => x.GetAllPublished()).ReturnsAsync(posts);

            var result = _postController.GetAllPublished().Result;
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void GetAllPublished_NotExistPosts_Return404()
        {
            var posts = new List<Post>()
            {
                new Post() { PostId = 5, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 1, UserId = 1, CreationDate = DateTime.Now },
                new Post() { PostId = 7, Title = "UnitTest title", TextContent = "UnitTest content", PostStatusId = 2, UserId = 3, CreationDate = DateTime.Now }
            };
            _mockPostService.Setup(x => x.GetAllPublished()).Throws<Exception>();

            var result = _postController.GetAllPublished().Result;
            var notFoundResult = result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, notFoundResult?.StatusCode);
        }
    }
}

