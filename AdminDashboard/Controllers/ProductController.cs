using AdminDashboard.Helper;
using AdminDashboard.Models.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index()
        {
            var products = await _unitOfWork.Repositry<Product, int>().GetAllAsync();
           var mappedproduct =   _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(mappedproduct);
        }
        public async Task <IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image != null)
                {
                    model.PictureUrl = PictureSetting.UploadFile(model.Image, "products");

                }
                else
                {
                    model.PictureUrl = @"images/products/Kulfi.png";

                }
                var mappedproduct = _mapper.Map<ProductViewModel, Product>(model);

                await _unitOfWork.Repositry<Product,int>().AddAsync(mappedproduct);
               var count = await _unitOfWork.CompleteAsync();
                if (count > 0) 
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _unitOfWork.Repositry<Product, int>().GetAsync(id);
            var mappedproduct = _mapper.Map<Product, ProductViewModel>(products);
            return View(mappedproduct);
        }
        [HttpPost]
        public async Task <IActionResult> Edit(int id , ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Image != null)
                    {
                        if (model.PictureUrl != null)
                        {
                            PictureSetting.DeleteFile(model.PictureUrl, "products");
                        }
                        model.PictureUrl=
                             PictureSetting.UploadFile(model.Image, "produtcs");
                       var mapedp = _mapper.Map<ProductViewModel, Product>(model);
                        _unitOfWork.Repositry<Product, int>().Update(mapedp);
                        var Result = await _unitOfWork.CompleteAsync();
                        if (Result > 0)
                        {
                            return RedirectToAction("Index");

                        }
                    }
                }catch(Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
               
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var products = await _unitOfWork.Repositry<Product, int>().GetAsync(id);
            var mappedproduct = _mapper.Map<Product, ProductViewModel>(products);
            return View(mappedproduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel model)
        {
            try
            {
                if (model.Id != id)
                {
                    return NotFound();
                }
                var product = await _unitOfWork.Repositry<Product, int>().GetAsync(id);
                if (product.PictureUrl != null)
                {
                    PictureSetting.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.Repositry<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
			catch (Exception ex)
			{

                return View(model);
			}

		}
	
	}
}
