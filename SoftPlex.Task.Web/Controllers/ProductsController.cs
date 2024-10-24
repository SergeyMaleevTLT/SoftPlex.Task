using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Web.Models;
using SoftPlex.Task.Web.Services;

namespace SoftPlex.Task.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService, IMapper mapper)
    {
        _logger = logger;
        _productService = productService;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index(string? searchName)
    {
        var products = await _productService.GetProductsSearchByNameAsync(searchName);
        return View(products);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return PartialView(new ProductPostViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductPostViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.AddProductAsync(_mapper.Map<ProductPostDto>(vm));

            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        
        return PartialView(_mapper.Map<ProductViewModel>(response));
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync(_mapper.Map<ProductDto>(vm));
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        
        await _productService.DeleteProductAsync(id);

        return RedirectToAction(nameof(Index));
    }
}