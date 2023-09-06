﻿using System;
namespace BlogEngine.Core.Entities
{
	public class PostStatus
	{
		public int PostStatusId { get; set; }
		public string Name { get; set; }

		public ICollection<Post> Post { get; set; } = new List<Post>();
    }
}

