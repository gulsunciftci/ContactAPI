using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactsAPI.Models
{
	public class Category
	{
		[Key]
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }

		[JsonIgnore]
		public ICollection<Contact> Contacts { get; set; }
	}
}
