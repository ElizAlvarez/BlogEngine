using System.ComponentModel.DataAnnotations;

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

		public PostStatus? PostStatus { get; set; }
		public User? User { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}

