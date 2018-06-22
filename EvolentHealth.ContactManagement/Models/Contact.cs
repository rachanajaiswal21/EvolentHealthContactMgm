using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvolentHealth.ContactManagement.Models
{
    public class Contact
    {
       
        public int ContactId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]      
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public Contact()
        {
            ContactId = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            IsActive = false;

        }


    }
}