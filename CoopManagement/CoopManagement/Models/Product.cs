using System;
using System.ComponentModel.DataAnnotations;

namespace CoopManagementApp.Models
{
    public class Product
    {
        [Key]
        public int id_product { get; set; }

        [Required]
        [StringLength(100)]
        public string name_product { get; set; }

        [Range(0, 99999.99)]
        public decimal cost { get; set; }

        public int total_product { get; set; }

        public DateTime receive_date { get; set; }
    }
}