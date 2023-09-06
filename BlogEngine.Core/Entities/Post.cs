using System;
namespace BlogEngine.Core.Entities
{
	public class Post
	{
		public int PostId { get; set; }
		public string Title { get; set; }
		public string TextContent { get; set; }
		public DateTime CreationDate { get; set; }
		public int PostStatusId { get; set; }
		public int UserId { get; set; }

		public PostStatus PostStatus { get; set; } = null!;
		public User User { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}

