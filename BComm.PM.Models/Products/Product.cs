using BComm.PM.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BComm.PM.Models.Products
{
    [Table("products", Schema = "bcomm_pm")]
    public class Product : WithHashId
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public string ImageDirectory { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public double StockQuantity { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public string ShopId { get; set; }
    }
}
