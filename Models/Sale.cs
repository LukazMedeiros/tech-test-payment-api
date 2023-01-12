namespace tech_test_payment_api.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public Guid CustomerId { get; set; }
        public List<string> Items { get; set; }
        public DateTime Date { get; set; }
        public EnumSaleStatus Status { get; set; }
    }
}