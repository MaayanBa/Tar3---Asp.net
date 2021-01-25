using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd___Asp.DTO
{
    public class ProductDTO
    {
        public string Name;
        public string SerialNumber;
        public double? Price;
        public double? Cost;
        public int? Inventory;
        public int? SupplierId;
    }
}