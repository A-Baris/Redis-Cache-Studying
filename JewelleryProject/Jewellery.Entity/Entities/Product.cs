using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Entity.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Gram { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        

    }
}
