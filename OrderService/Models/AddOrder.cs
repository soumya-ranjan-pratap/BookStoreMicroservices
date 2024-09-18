namespace OrderService.Models
{
    public class AddOrder
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
