using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerCore.Models
{
    public class UserOrderHeader
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? ApplicationUser { get; set; }

        public DateTime DateOfOrder { get; set; }

        public DateTime DateOfShipped { get; set; }

        public float TotalOrderAmount { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Carrier { get; set; }

        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }

        public DateTime? PaymentProcessDate { get; set; }

        public string? PaymentMethod { get; set; }

        //public int TransiactionId { get; set; }
        //[ForeignKey(nameof(TransiactionId))]
        //public Transiaction? Transiaction { get; set; }

        public string PhoneNumber { get; set; }

        public string DeliveryStreet { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Name { get; set; }

    }
}
