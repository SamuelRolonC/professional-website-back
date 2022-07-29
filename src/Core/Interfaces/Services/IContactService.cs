using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IContactService
    {
        Task<Contact> InsertAsync(Contact contact);
        Task<ResultData> ProcessFormAsync(Contact contact);
    }
}
