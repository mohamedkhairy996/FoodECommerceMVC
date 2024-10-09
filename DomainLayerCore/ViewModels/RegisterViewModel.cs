using DomainLayerCore.Models;
using System.ComponentModel.DataAnnotations;

namespace DomainLayerCore.ViewModels
{
    public class RegisterUserViewModel
    {
        public ApplicationUser applicationUser { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
