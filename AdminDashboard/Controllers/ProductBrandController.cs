using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;

namespace AdminDashboard.Controllers
{
	public class ProductBrandController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductBrandController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task <IActionResult> Index()
		{
			var brands = await _unitOfWork.Repositry<ProductBrand, int>().GetAllAsync();
			return View(brands);
		}
		public async Task <IActionResult> Create(ProductBrand brand)
		{
			try
			{
				await _unitOfWork.Repositry<ProductBrand, int>().AddAsync(brand);
				await _unitOfWork.CompleteAsync();
				return RedirectToAction("Index");
			}catch(Exception e)
			{
				ModelState.AddModelError("Name", "please Enter New Name");
				return View("Index", await _unitOfWork.Repositry<ProductBrand, int>().GetAllAsync());
			}
		}
		public async Task<IActionResult>Delete(int id)
		{
			var brand = await _unitOfWork.Repositry<ProductBrand, int>().GetAsync(id);
			 _unitOfWork.Repositry<ProductBrand, int>().Delete(brand);
			await _unitOfWork.CompleteAsync();
			return RedirectToAction("Index");
		}
	}
}
