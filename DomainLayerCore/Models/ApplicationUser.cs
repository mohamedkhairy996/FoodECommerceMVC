﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DomainLayerCore.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string Address { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        [Required]
        public string? State { get; set; }


    }
}
