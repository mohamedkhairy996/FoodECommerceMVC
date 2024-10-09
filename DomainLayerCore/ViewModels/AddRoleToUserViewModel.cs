using DomainLayerCore.Models;
using Microsoft.AspNetCore.Identity;

namespace DomainLayerCore.ViewModels
{
    public class AddRoleToUserViewModel
    {
        public List<ApplicationUser>? Users { get; set; }

        public string? SelectedUser { get; set; }

        public List<IdentityRole>? Roles { get; set; }

        public string? SelectedRole { get; set; }
    }
}
