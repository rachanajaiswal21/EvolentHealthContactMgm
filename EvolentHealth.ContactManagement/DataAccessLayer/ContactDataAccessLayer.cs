using EvolentHealth.ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EvolentHealth.ContactManagement.BusinessLayer
{
    public class ContactDataAccessLayer : IContactDataAccessLayer
    {
        private EvolentHealthContactManagementContext db = new EvolentHealthContactManagementContext();
        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }

        public Contact GetContactById(int id)
        {
            Contact contact = db.Contacts.Find(id);

            return contact;


        }
        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.ContactId == id) > 0;
        }


        public Contact UpdateContact(int id, Contact contact)
        {
            if (id != contact.ContactId)
            {
                return null;
            }

            db.Entry(contact).State = EntityState.Modified;
 
            db.SaveChanges();
            
            return  contact ;
             
        }

        public Contact AddContact(Contact contact)
        {
            var list = db.Contacts;
            int newId = 0;
            newId = list.Max(p => p.ContactId);
            newId++;
            contact.ContactId = newId;
           
            db.Contacts.Add(contact);
            db.SaveChanges();

            return contact;
        }

        public Contact DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return null;
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return contact ;
        }

        

    }

 
}