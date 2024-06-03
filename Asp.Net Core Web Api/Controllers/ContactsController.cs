using Asp.Net_Core_Web_Api.Data;
using Asp.Net_Core_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_Core_Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute]Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact is not null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email; 
                contact.Phone = updateContactRequest.Phone;
                await dbContext.SaveChangesAsync();

                return Ok(contact); 
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute]Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact is not null)
            {
                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact is not null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
