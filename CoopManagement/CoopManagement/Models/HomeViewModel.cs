namespace CoopManagementApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Transaction>? Transactions { get; set; }
    }
}