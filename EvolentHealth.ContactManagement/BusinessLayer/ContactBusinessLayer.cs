using EvolentHealth.ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace EvolentHealth.ContactManagement.BusinessLayer
{
    public class ContactBusinessLayer : IContactBusinessLayer
    {
        private IContactDataAccessLayer _objContactDal;
        public ContactBusinessLayer(IContactDataAccessLayer objContactDal)
        {
            _objContactDal = objContactDal;
        }

        public IQueryable<Contact> GetContacts()
        {
           return  _objContactDal.GetContacts();
        }

        public Contact GetContactById(int id)
        {

            return _objContactDal.GetContactById(id);
        }
       public Contact UpdateContact(int id, Contact contact)
        {
            return _objContactDal.UpdateContact(id, contact);
        }
     
        public Contact AddContact(Contact contact)
        {
            return _objContactDal.AddContact(contact);
        }
        public Contact DeleteContact(int id)
        {
            return _objContactDal.DeleteContact(id);
        }
    }
}