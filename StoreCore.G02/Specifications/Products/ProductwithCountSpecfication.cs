using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications.Products
{
    public class ProductwithCountSpecfication:BaseSpecification<Product,int>
    {
        public ProductwithCountSpecfication(Productspecparms productspec) : base
          (
    p =>
                        (string.IsNullOrEmpty(productspec.Search)||p.Name.ToLower().Contains(productspec.Search))
                         &&
                        (!productspec.BrandId.HasValue || productspec.BrandId == p.BrandId)
                        &&
                        (!productspec.TypeId.HasValue || productspec.TypeId == p.TypeId)
         )
        {



        }
        
        

    }
}
