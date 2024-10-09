using StoreCore.G02.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCore.G02.Specifications.Products
{
    public class ProductSpecfication:BaseSpecification<Product,int>
    {

        public ProductSpecfication(int id):base(p => p.Id == id)
        {
            Applyincludes();
        }
        //900
        //p.z =50
        //p.i =2
        public ProductSpecfication(Productspecparms productspec) :base(
            p=>
            (string.IsNullOrEmpty(productspec.Search)||p.Name.ToLower().Contains(productspec.Search))
            &&
            (!productspec.BrandId.HasValue || productspec.BrandId==p.BrandId)
            &&
            (!productspec.TypeId.HasValue || productspec .TypeId== p.TypeId)
            )
        {
            //name ,price asc, price dec
            if (!string.IsNullOrEmpty(productspec.sort))
            {
                switch (productspec.sort) 
                {
                    
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }

            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            Applyincludes();
            //pagesize ,pageindex
            ApplyPagination(productspec.Pagesize*(productspec.PageIndex-1),productspec.Pagesize);
        }
        private void Applyincludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
