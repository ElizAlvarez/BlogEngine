using System;
namespace BlogEngine.Core.Entities
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int RoleId { get; set; }

		public Role Role { get; set; } = null!;
		public Post Post { get; set; } = null!;
	}
}

