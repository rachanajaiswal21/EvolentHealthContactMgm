using EvolentHealth.ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolentHealth.ContactManagement.BusinessLayer
{
   public interface IContactDataAccessLayer
    {
        IQueryable<Contact> GetContacts();
        Contact GetContactById(int id);
        Contact UpdateContact(int id, Contact contact);
    
        Contact AddContact(Contact contact);
        Contact DeleteContact(int id);
    }
}
