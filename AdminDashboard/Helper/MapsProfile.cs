using AdminDashboard.Models.Products;
using AutoMapper;
using StoreCore.G02.Entites;

namespace AdminDashboard.Helper
{
    public class MapsProfile:Profile
    {
        public MapsProfile()
        {
			CreateMap<  Product,ProductViewModel>().ReverseMap();
			//	ForMember(d => d.ProductBrandName
			//, option =>
			//option.MapFrom(s => s.Brand.Name))
				
			//	.ForMember(d => d.ProductTypeName, option
			//	=> option.MapFrom(s => s.Type.Name)).ReverseMap();

			//CreateMap<ProductViewModelCreate, Product>().ReverseMap();
		}
	}
}
