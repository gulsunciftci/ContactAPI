using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly ContactsAPIDbContext _dbContext;
		public ContactsController(ContactsAPIDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetContacts()
		{
			return Ok(await _dbContext.Contacts.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetContact( [FromRoute]int id)
		{
			var contact = await _dbContext.Contacts.FindAsync(id);
			if (contact == null)
			{
				return NotFound();
			}

			return Ok(contact);
		}


		[HttpPost]
		public async Task<IActionResult> AddContact([FromBody] AddContextRequest addContextRequest)
		{
			var contact = new Contact()
			{

				Address = addContextRequest.Address,
				Email = addContextRequest.Email,
				FullName = addContextRequest.FullName,
				Phone = addContextRequest.Phone
			};
			await _dbContext.Contacts.AddAsync(contact);
			await _dbContext.SaveChangesAsync();

			return Ok(contact);
		}

		[HttpPut("{id}")]

		public async Task<IActionResult> UpdateContact(int id, UpdateContextRequest updateContextRequest)
		{
			var contact = await _dbContext.Contacts.FindAsync(id);

			if (contact != null)
			{

				contact.Address = updateContextRequest.Address;
				contact.Email = updateContextRequest.Email;
				contact.FullName = updateContextRequest.FullName;
				contact.Phone = updateContextRequest.Phone;


				await _dbContext.SaveChangesAsync();
				return Ok(contact);
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteContact(int id)
		{
			var contact = await _dbContext.Contacts.FindAsync(id);

			if (contact != null)
			{

				_dbContext.Remove(contact);
				await _dbContext.SaveChangesAsync();
				return Ok(contact);
			}

			return NotFound();
		}

	}
}
