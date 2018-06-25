using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolentHealth.ContactManagement.Controllers;
using System.Collections.Generic;
using EvolentHealth.ContactManagement.Models;
using System.Web.Http.Results;

namespace EvolentHealth.ContactManagement.Tests
{
    [TestClass]
    public class TestContactController
    {
        [TestMethod]
        public void GetAllContacts_ShouldReturnAllProducts()
        {
            var testContacts = GetTestContacts();
            var controller = new  ContactsController(testContacts);

            var result = controller.GetContacts() as List<Contact>;
            Assert.AreEqual(testContacts.Count, result.Count);
        }

       
        [TestMethod]
        public void GetContact_ShouldReturnCorrectContact()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactsController(testContacts);

            var result = controller.GetContact(4) as OkNegotiatedContentResult<Contact>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testContacts[3].FirstName, result.Content.FirstName);
        }

     
        [TestMethod]
        public void GetContact_ShouldNotFindContact()
        {
            var controller = new ContactsController(GetTestContacts());

            var result = controller.GetContact(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Contact> GetTestContacts()
        {
            var testContacts = new List<Contact>();
            testContacts.Add(new Contact { FirstName = "Peter", LastName = "Parker", Email = "paprker@gmail.com" , PhoneNumber = "342-388-8892", IsActive=true });
            testContacts.Add(new Contact { FirstName = "John", LastName = "Smith", Email = "jsmith@gmail.com", PhoneNumber = "567-344-8892", IsActive = true });
            testContacts.Add(new Contact { FirstName = "Will", LastName = "White", Email = "wwhite@gmail.com", PhoneNumber = "342-388-4543", IsActive = false });
            testContacts.Add(new Contact { FirstName = "Jack", LastName = "Smith", Email = "jsmith@gmail.com", PhoneNumber = "445-388-8892", IsActive = false });

            return testContacts;
        }
    }
}
