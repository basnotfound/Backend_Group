using System.ComponentModel.DataAnnotations;

namespace CoopManagementApp.Models
{
    public class Customer
    {
        [Key]
        public int id_cus { get; set; }

        [Required]
        [StringLength(100)]
        public string name_cus { get; set; }
    }
}