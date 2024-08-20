using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biomarkt_App_WPF
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }

        public Product(string productName, string productBrand, string productCategory, float productPrice)
        {
            Name = productName;
            Brand = productBrand;
            Category = productCategory;
            Price = productPrice;
        }
    }
}
