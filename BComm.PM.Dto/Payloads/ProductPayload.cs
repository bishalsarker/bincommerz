using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class ProductPayload
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double StockQuantity { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
