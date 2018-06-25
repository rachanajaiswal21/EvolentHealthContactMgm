using EvolentHealth.ContactManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolentHealth.ContactManagement.Tests
{
    class TestContactDbSet : TestDbSet<Contact>
    {
        public override Contact Find(params object[] keyValues)
        {
            return this.SingleOrDefault(contact => contact.ContactId == (int)keyValues.Single());
        }
    }
}
