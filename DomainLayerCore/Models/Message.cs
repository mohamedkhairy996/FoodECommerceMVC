using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayerCore.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public bool AdminMsg { get; set; }
        public bool IsRead { get; set; }

        public int? HeaderId { get; set; }  // ID of the Header
        [ForeignKey(nameof(HeaderId))]
        public virtual MessageHeader? MessageHeader { get; set; }

        public DateTime CreatedAt { get; set; }
        
        
    }
}
