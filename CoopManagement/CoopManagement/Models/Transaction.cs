using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoopManagementApp.Models
{
    public class Transaction
    {
        [Key]
        public int id_transaction { get; set; }

        [ForeignKey("Customer")]
        public int? id_cus { get; set; }

        [ForeignKey("Product")]
        public int? id_product { get; set; }

        public int quantity { get; set; }

        [Range(0, 99999.99)]
        public decimal total_price { get; set; }

        public DateTime date_transaction { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}