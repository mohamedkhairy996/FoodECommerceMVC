using DomainLayerCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomainLayerCore.ViewModels
{
    public class SummaryViewModel
    {

        public IEnumerable<UserCart>? UserCartList { get; set; }

        public UserOrderHeader OrderHeaderSummary { get; set; }

        public string? CartUserId { get; set; }

        public IEnumerable<SelectListItem>? PaymentMethod { get; set; }

        public string? SelectedPaymentMethod { get; set; }
    }
}
