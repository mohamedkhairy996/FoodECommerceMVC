using System.ComponentModel.DataAnnotations;

namespace DomainLayerCore.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
