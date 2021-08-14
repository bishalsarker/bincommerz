using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Products
{
    public class ProductStockResponse
    {
        public IEnumerable<ProductResponse> OutOfStock { get; set; }

        public IEnumerable<ProductResponse> Warning { get; set; }
    }
}
