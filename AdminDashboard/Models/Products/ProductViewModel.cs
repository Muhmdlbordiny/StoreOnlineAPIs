using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StoreCore.G02.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboard.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ProductTypeId is Required")]
        public int ProductTypeId { get; set; }

        public ProductType? Type { get; set; }

        [Required(ErrorMessage = "ProductBrandId is Required")]

        public int ProductBrandId { get; set; }
        public ProductBrand? Brand { get; set; }

    }
}
