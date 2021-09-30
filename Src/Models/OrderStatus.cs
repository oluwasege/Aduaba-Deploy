using System.ComponentModel.DataAnnotations;

namespace Aduaba.Models
{
    public class OrderStatus
    {
        
        public string Id { get; set; }
        
        public string Status { get; set; }
        
        public bool PaymentStatus { get; set; }

        
        public bool HasBeenShipped { get; set; }
    }
}