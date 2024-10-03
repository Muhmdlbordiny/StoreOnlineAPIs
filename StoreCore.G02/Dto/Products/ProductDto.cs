using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Dto.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }//Fk

        public int? BrandId { get; set; }//FK
        public string BrandName { get; set; }
    }
}
