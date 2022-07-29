using Core;
using Core.Entities;
using Core.Interfaces.Services;
using EmailService;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ContactService : IContactService
    {
        private readonly ContactRepository _contactRepository;
        private readonly IEmailSender _emailSender;

        public ContactService(ContactRepository contactRepository
            , IEmailSender emailSender)
        {
            _contactRepository = contactRepository;
            _emailSender = emailSender;
        }

        public async Task<Contact> InsertAsync(Contact contact)
        {
            try
            {
                return await _contactRepository.InsertAsync(contact);
            }
            catch(Exception e)
            {
                // TODO Log
                throw;
            }
        }

        public async Task<ResultData> ProcessFormAsync(Contact contact)
        {
            try
            {
                var errorData = await ValidateContact(contact);
                if (errorData.HasErrors())
                    return errorData;

                contact = await _contactRepository.InsertAsync(contact);

                await _emailSender.SendEmailForContactAsync(
                    new EmailMessage(contact.Name, contact.Email, contact.Subject, contact.Content));

                return errorData;
            }
            catch (Exception e)
            {
                // TODO Log
                throw;
            }
        }

        private async Task<ResultData> ValidateContact(Contact contact)
        {
            var errorData = new ResultData();

            var count = await _contactRepository.FindNotReadLastMonthAsync(contact);
            if (count > 3)
            {
                errorData.AddError("ContactLimitExceded", "You already have messages awaiting for a response.");
                return errorData;
            }

            if (contact.Name.Length < 3)
                errorData.AddError("NameInvalid", "Name is empty or too short.");

            if (!contact.IsValidEmail())
                errorData.AddError("EmailInvalid", "Email entered is not valid.");

            if (contact.Content.Length < 3)
                errorData.AddError("ContentInvalid", "Message content is empty or too short.");


            return errorData;
        }
    }
}
