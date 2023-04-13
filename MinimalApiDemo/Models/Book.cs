using System;
namespace MinimalApiDemo.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool IsActive { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? LastUpdated { get; set; }
	}
}

