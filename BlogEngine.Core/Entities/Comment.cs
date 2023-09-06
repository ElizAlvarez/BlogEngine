using System;
namespace BlogEngine.Core.Entities
{
	public class Comment
	{
		public int CommentId { get; set; }
		public int PostId { get; set; }
		public string CommentContent { get; set; }
		public DateTime CreationDate { get; set; }
		public int UserId { get; set; }
		public bool IsByEditor { get; set; }

		public User User { get; set; }
		public Post Post { get; set; }
	}
}

