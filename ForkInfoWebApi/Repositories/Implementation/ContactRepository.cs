using ForkInfoWebApi.Data;
using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ForkInfoWebApi.Repositories.Implementation
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ContactRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            await this.applicationDbContext.Contact.AddAsync(contact);
            await this.applicationDbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await this.applicationDbContext.Contact.ToListAsync();
        }
    }
}
