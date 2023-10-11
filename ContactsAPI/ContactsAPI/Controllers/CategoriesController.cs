using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ContactsAPIDbContext _dbContext;
        public CategoriesController(ContactsAPIDbContext dbContext)
        {
			_dbContext = dbContext;
		}


		[HttpGet]
		public async Task<IActionResult> GetContacts() //listeleme
		{
			return Ok(await _dbContext.Categories.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetContact([FromRoute] int id) //id'ye göre listeleme
		{
			var category = await _dbContext.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}


		[HttpPost]
		public async Task<IActionResult> AddContact([FromBody] AddCategoryContextRequest addContextRequest)  //ekleme
		{
			var category = new Category()
			{
                 CategoryName=addContextRequest.CategoryName
			};
			await _dbContext.Categories.AddAsync(category);
			await _dbContext.SaveChangesAsync();

			return Ok(category);
		}

		[HttpPut("{id}")]

		public async Task<IActionResult> UpdateContact(int id, UpdateCategoryContextRequest updateContextRequest) //id'ye göre güncelleme
		{
			var category = await _dbContext.Categories.FindAsync(id);

			if (category != null)
			{

				category.CategoryName = updateContextRequest.CategoryName;


				await _dbContext.SaveChangesAsync();
				return Ok(category);
			}

			return NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteContact(int id) //id'ye göre silme
		{
			var category = await _dbContext.Categories.FindAsync(id);

			if (category != null)
			{

				_dbContext.Remove(category);
				await _dbContext.SaveChangesAsync();
				return Ok(category);
			}

			return NotFound();
		}
	}
}
