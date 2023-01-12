namespace tech_test_payment_api.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid Seller { get; set; }
        public Guid Customer { get; set; }
        public List<string> Items { get; set; }
        public DateTime Date { get; set; }
        public EnumSaleStatus Status { get; set; }
    }
}