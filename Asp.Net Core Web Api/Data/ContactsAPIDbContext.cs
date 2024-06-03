using Asp.Net_Core_Web_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_Core_Web_Api.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
