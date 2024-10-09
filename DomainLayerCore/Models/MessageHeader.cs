using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayerCore.Models
{
    public class MessageHeader
    {
        public int Id { get; set; }

        public string? User1_Id { get; set; }  // ID of the sender
        [ForeignKey(nameof(User1_Id))]
        public virtual ApplicationUser? User_1 { get; set; }

        public string? CustomerName {  get; set; }

        public string? User2_Id { get; set; }    // ID of the receiver
        [ForeignKey(nameof(User2_Id))]
        public virtual ApplicationUser? User_2 { get; set; }
    }
}
