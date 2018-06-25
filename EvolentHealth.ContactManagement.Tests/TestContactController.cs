using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolentHealth.ContactManagement.Controllers;
using System.Collections.Generic;
using EvolentHealth.ContactManagement.Models;
using System.Web.Http.Results;
using EvolentHealth.ContactManagement.BusinessLayer;
using System.Net;

namespace EvolentHealth.ContactManagement.Tests
{
    [TestClass]
    public class TestContactController
    {
        [TestMethod]
        public void GetAllContacts_ShouldReturnAllProducts()
        {
            var context = new TestContactAppContext();
            
            context.Contacts.Add(new Contact {ContactId =1,  FirstName = "Peter", LastName = "Parker", Email = "paprker@gmail.com", PhoneNumber = "342-388-8892", IsActive = true });
            context.Contacts.Add(new Contact { ContactId = 2, FirstName = "John", LastName = "Smith", Email = "jsmith@gmail.com", PhoneNumber = "567-344-8892", IsActive = true });
            context.Contacts.Add(new Contact { ContactId = 3, FirstName = "Will", LastName = "White", Email = "wwhite@gmail.com", PhoneNumber = "342-388-4543", IsActive = false });
            context.Contacts.Add(new Contact { ContactId = 4, FirstName = "Jack", LastName = "Smith", Email = "jsmith@gmail.com", PhoneNumber = "445-388-8892", IsActive = false });


            var controller = new ContactsController(context);
            var result = controller.GetContacts() as TestContactDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Local.Count);
        }

        [TestMethod]
        public void PostProduct_ShouldReturnSameProduct()
        {
            var controller = new ContactsController(new TestContactAppContext());

            var item = GetDemoContact();

            var result =
                controller.PostContact(item) as CreatedAtRouteNegotiatedContentResult<Contact>;

            Assert.IsNotNull(result);
            //Assert.AreEqual(result.RouteName, "DefaultApi");
           // Assert.AreEqual(result.RouteValues["ContactId"], result.Content.ContactId);
            // Assert.AreEqual(result.Content.FirstName, item.FirstName);
        }


        [TestMethod]
        public void GetContact_ShouldReturnProductWithSameID()
        {
            var context = new TestContactAppContext();
            context.Contacts.Add(GetDemoContact());

            var controller = new ContactsController(context);
            var result = controller.GetContact(3) as OkNegotiatedContentResult<Contact>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.ContactId);
        }


        [TestMethod]
        public void GetContact_ShouldNotFindContact()
        {
            var context = new TestContactAppContext();
            var controller = new ContactsController(context);
            var result = controller.GetContact(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutContact_ShouldReturnStatusCode()
        {
            var controller = new ContactsController(new TestContactAppContext());

            var item = GetDemoContact();

            var result = controller.PutContact(item.ContactId, item) as OkNegotiatedContentResult<Contact>;
            Assert.IsNotNull(result);
            Assert.AreEqual(item.ContactId, result.Content.ContactId);
        }

        [TestMethod]
        public void PutContact_ShouldFail_WhenDifferentID()
        {
            var controller = new ContactsController(new TestContactAppContext());

            var badresult = controller.PutContact(999, GetDemoContact());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }




        [TestMethod]
        public void DeleteProduct_ShouldReturnOK()
        {
            var context = new TestContactAppContext();
            var item = GetDemoContact();
            context.Contacts.Add(item);

            var controller = new ContactsController(context);
            var result = controller.DeleteContact(3) as OkNegotiatedContentResult<Contact>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.ContactId, result.Content.ContactId);
        }


        
        Contact GetDemoContact()
        {
            return new Contact() {ContactId = 3, FirstName = "Will", LastName = "White", Email = "wwhite@gmail.com", PhoneNumber = "342-388-4543", IsActive = false };
        }
    }

    internal class Mock<T>
    {
        public Mock()
        {
        }
    }
}
