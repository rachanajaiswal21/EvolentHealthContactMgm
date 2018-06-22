using EvolentHealth.ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EvolentHealth.ContactManagement
{
    public interface IContacttRepository
    {
        IQueryable<Contact> GetAll();
        Contact GetByID(int id);
        Contact Add(Contact contact);
        Contact Update(int id, Contact contact);
        Contact Delete(int id);
        bool ContactExists(int id);
    }
    public class ContacttRepository:IContacttRepository
    {
        private EvolentHealthContactManagementContext db = new EvolentHealthContactManagementContext();
        public IQueryable<Contact> GetAll()
        {
            return db.Contacts;
        }

        public Contact GetByID(int id)
        {
            Contact contact = db.Contacts.Find(id);
             
            return contact;
        }
        public Contact Add(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            return contact;
        }

        public Contact Update(int id, Contact contact)
        {
            try
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ContactExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return contact;


        }

        public Contact Delete(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return null;
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();
            return contact;
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.ContactId == id) > 0;
        }
    }
}