using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly ContactsAPIDbContext _dbContext;
		public ContactsController(ContactsAPIDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetContacts() //listeleme
		{
			return Ok(await _dbContext.Contacts.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetContactCategory(int id) //id'si verilen Contact'ın categorysinin listelenmesi
		{
			var contact = await _dbContext.Contacts.Where(x=>x.Id==id).Include("Category").Select(x=>x.Category.CategoryName).FirstOrDefaultAsync();
			return Ok(contact);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetContact( [FromRoute]int id) //id'ye göre listeleme
		{
			var contact = await _dbContext.Contacts.FindAsync(id);
			if (contact == null)
			{
				return NotFound();
			}

			return Ok(contact);
		}


		[HttpPost]
		public async Task<IActionResult> AddContact([FromBody] AddContactContextRequest addContextRequest)  //ekleme
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

		public async Task<IActionResult> UpdateContact(int id, UpdateContactContextRequest updateContextRequest) //id'ye göre güncelleme
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
		public async Task<IActionResult> DeleteContact(int id) //id'ye göre silme
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
