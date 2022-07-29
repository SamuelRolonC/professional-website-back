using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> InsertAsync(Contact contact);
        Task<long> FindNotReadLastMonthAsync(Contact contact);
    }
}
