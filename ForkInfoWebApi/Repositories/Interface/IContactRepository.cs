using ForkInfoWebApi.Models.Domain;

namespace ForkInfoWebApi.Repositories.Interface
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
    }
}
