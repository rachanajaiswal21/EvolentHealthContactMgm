using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EvolentHealth.ContactManagement.Models;
using EvolentHealth.ContactManagement.BusinessLayer;

namespace EvolentHealth.ContactManagement.Controllers
{
    public class ContactsController : ApiController
    {
        private IContactBusinessLayer _objContactBal;
       

        public ContactsController(IContactBusinessLayer objContactBal)
        {
            _objContactBal = objContactBal;
        }

        

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {
            // return db.Contacts;
            return _objContactBal.GetContacts();
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = _objContactBal.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ContactId)
            {
                return BadRequest();
            }

           

            try
            {
                contact = _objContactBal.UpdateContact(id, contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (contact == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(contact);
            
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            contact = _objContactBal.AddContact(contact);
            return CreatedAtRoute("DefaultApi", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = _objContactBal.DeleteContact(id);
            if (contact == null)
            {
                return NotFound();
            }

          

            return Ok(contact);
        }

        

        
    }
}