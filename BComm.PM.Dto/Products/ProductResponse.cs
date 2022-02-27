using BComm.PM.Dto.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Products
{
    public class ProductResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ImageResponse> Images { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double DiscountInPercentage { get; set; }

        public bool InStock { get; set; }

        public double StockQuantity { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
