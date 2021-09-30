namespace Aduaba.Models
{
    public class OrderDetail
    {
        public string OrderDetailId { get; set; }

        public string OrderId { get; set; }

        public string Username { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}