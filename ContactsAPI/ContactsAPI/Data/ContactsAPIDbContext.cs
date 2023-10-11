using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
	public class ContactsAPIDbContext:DbContext
	{
        public ContactsAPIDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
		public DbSet<Category> Categories { get; set; }

	}
}
