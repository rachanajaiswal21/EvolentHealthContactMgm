using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EvolentHealth.ContactManagement.Models;
using EvolentHealth.ContactManagement.BusinessLayer;

namespace EvolentHealth.ContactManagement.Tests
{
    public  class TestContactAppContext : IContactBusinessLayer
    {
        public TestContactAppContext()
        {
            this.Contacts = new TestContactDbSet();
        }

        public DbSet<Contact> Contacts { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Contact item) { }
        public void Dispose() { }

        public IQueryable<Contact> GetContacts()
        {
            return this.Contacts;
        }

        public Contact GetContactById(int id)
        {
            Contact contact = this.Contacts.Find(id);

            return contact;
        }

        public Contact UpdateContact(int id, Contact contact)
        {
             
            this.MarkAsModified(contact);
            return contact;
        }

        public Contact AddContact(Contact contact)
        {
           
            this.Contacts.Add(contact);
            this.SaveChanges();

            return contact;
        }

        public Contact DeleteContact(int id)
        {
            Contact contact = this.Contacts.Find(id);
            if (contact == null)
            {
                return null;
            }

            this.Contacts.Remove(contact);
           
            return contact;
        }
    }
}
