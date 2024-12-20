using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Entites
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }
        public int TypeId { get; set; }
        public ProductType? Type { get; set; }//Fk

        public int BrandId { get; set; }//FK
        public ProductBrand? Brand { get; set; }

       

    }
}
