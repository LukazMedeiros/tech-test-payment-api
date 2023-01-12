namespace tech_test_payment_api.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Seller Seller { get; set; }
        public Customer Customer { get; set; }
        public List<string> Items { get; set; }
        public DateTime Date { get; set; }
        public EnumSaleStatus Status { get; set; }
    }
}