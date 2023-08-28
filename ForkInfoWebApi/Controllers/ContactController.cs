using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Models.DTO;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForkInfoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ContactSendDTO>> PostContact(ContactGetDTO contactGetDTO)
        {
            var contact = new Contact
            {
                Name= contactGetDTO.Name,
                Gender= contactGetDTO.Gender,
                Email= contactGetDTO.Email,
                Education= contactGetDTO.Education,
                Message= contactGetDTO.Message,
                Mobile= contactGetDTO.Mobile
            };

            contact = await this.contactRepository.CreateContactAsync(contact);

            var response = new ContactSendDTO
            {
                Id = contact.Id,
                Name= contact.Name,
                Gender= contact.Gender,
                Email= contact.Email,
                Education= contact.Education,
                Message= contact.Message,
                Mobile= contact.Mobile
            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<ActionResult<List<ContactSendDTO>>> GetAllContacts()
        {
            var contacts = await this.contactRepository.GetAllContactsAsync();

            var response = new List<ContactSendDTO>();

            foreach (var contact in contacts)
            {
                response.Add(new ContactSendDTO
                {
                    Id = contact.Id,
                    Name= contact.Name,
                    Gender= contact.Gender,
                    Email= contact.Email,
                    Education= contact.Education,
                    Message= contact.Message,
                    Mobile= contact.Mobile
                });
            }

            return Ok(response);
        }

    }
}
