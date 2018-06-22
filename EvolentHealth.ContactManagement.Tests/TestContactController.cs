using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolentHealth.ContactManagement.Controllers;
using System.Collections.Generic;
using EvolentHealth.ContactManagement.Models;

namespace EvolentHealth.ContactManagement.Tests
{
    [TestClass]
    public class TestContactController
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testContacts = GetTestContacts();
            var controller = new  ContactsController(testContacts);

            var result = controller.GetAllProducts() as List<Contact>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = await controller.GetAllProductsAsync() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].Name, result.Content.Name);
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts[3].Name, result.Content.Name);
        }

        [TestMethod]
        public void GetProduct_ShouldNotFindProduct()
        {
            var controller = new SimpleProductController(GetTestProducts());

            var result = controller.GetProduct(999);
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
