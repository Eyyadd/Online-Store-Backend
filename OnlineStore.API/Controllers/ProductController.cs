using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Application.Helper;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;
using OnlineStore.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        //private readonly IFilterService _filterService;

        public ProductController(IProductServices productServices
          /*  IFilterService filterService*/)
        {
            _productServices = productServices;
            //_filterService = filterService;
        }

        [HttpGet]
        public async Task<GeneralResponse<PaginationDTO<ProductElementDTO>>> All(int pageSize , int pageIndex)
        {
            //try
            //{
                var products =await  _productServices.AllProducts(pageSize , pageIndex);

                if (products == null || products.Items.Count() == 0)
                {
                    var notFoundResponse = new GeneralResponse<List<ProductElementDTO>>(false, "No products found");
                return new GeneralResponse<PaginationDTO<ProductElementDTO>>(false , "There Are Not Products");
                }

                var response = new GeneralResponse<PaginationDTO<ProductElementDTO>>(true, "Products retrieved successfully", products);
                return response;
            //}
            //catch (Exception ex)
            //{
            //    var errorResponse = new GeneralResponse<List<ProductElementDTO>>(false, $"Error retrieving products: {ex.Message}");
            //    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            //}
        }


        [HttpGet("{id}")]
        public ActionResult<GeneralResponse<ProductDetailsDTO>> ProductById(int id)
        {
            try
            {
                var productDetails = _productServices.ProductDetails(id);
                if (productDetails == null)
                {
                    var notFoundResponse = new GeneralResponse<ProductDetailsDTO>(false, "Product not found");
                    return NotFound(notFoundResponse);
                }

                var response = new GeneralResponse<ProductDetailsDTO>(true, "Product details retrieved successfully", productDetails);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new GeneralResponse<ProductDetailsDTO>(false, $"Error retrieving product details: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpGet("GetByCategory")]
        public ActionResult<GeneralResponse<IEnumerable<ProductElementDTO>>> ProudctByCategory(int categoryId)
        {
            GeneralResponse<IEnumerable<ProductElementDTO>> response = new GeneralResponse<IEnumerable<ProductElementDTO>>(false, "Invalid Category ID!");
            var Result = _productServices.ProductsByCategoryId(categoryId);
            if (Result is not null)
            {
                response.Success = true;
                response.Message = "Products Retrives Successfully";
                response.Data = Result;
                return response;
            }
            return response;
        }

        [HttpGet("Seller")]
        public GeneralResponse<IEnumerable<string>> Seller()
        {
          var Result =  _productServices.ProuctsSaller();
            return new GeneralResponse<IEnumerable<string>>(true, "", Result);
        }

        [HttpPost("Create")]
        public async Task<GeneralResponse<ProductElementDTO>> Add(CreateProductDTO createProductDTO )
        {
            
            var Result =  _productServices.CreateProduct(createProductDTO );
            return new GeneralResponse<ProductElementDTO>(true, "", Result);

        }

        [HttpGet("BestSeller")]
        public GeneralResponse<IEnumerable<ProductElementDTO>> BestSeller(int size)
        {
            var Result = _productServices.BestSellerProducts(size);
            return new GeneralResponse<IEnumerable<ProductElementDTO>>(true, "", Result);
        }



        [HttpGet("NewArrival")]
        public GeneralResponse<IEnumerable<ProductElementDTO>> NewArrival(int size)
        {
            var Result = _productServices.NewArraivelProducts(size);
            return new GeneralResponse<IEnumerable<ProductElementDTO>>(true, "", Result);
        }

        [HttpGet("ProductHaveSale")]
        public GeneralResponse<IEnumerable<ProductElementDTO>> HaveSale()
        {
            var Result = _productServices.SaleProducts();
            return new GeneralResponse<IEnumerable<ProductElementDTO>>(true, "", Result);
        }

        [HttpGet("GetByCategoryType")]
        public GeneralResponse<IEnumerable<ProductElementDTO>> GetByCategoryType(string categoryType)
        {
            var Result = _productServices.GetByCategoryType(categoryType);
            return new GeneralResponse<IEnumerable<ProductElementDTO>>(true, "", Result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Set a path to save the file (e.g., "wwwroot/images")
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            // Save the file
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var Result = Path.Combine("https://localhost:44322/", "wwwroot/images", file.FileName);
            return Ok(new { message = "File uploaded successfully", filePath = Result });
        }



        [HttpPost("CreateProductVariant")]
        public GeneralResponse<CreateProductVariantDTO> CreateProductVariant(CreateProductVariantDTO createProductVariantDTO)
        {
            var Result = _productServices.CreateProductVariant(createProductVariantDTO);
            if(Result is not null)
            {
                return new GeneralResponse<CreateProductVariantDTO>(true, "", Result);
            }
            return new GeneralResponse<CreateProductVariantDTO>(false, "Something Went Wrong");
        }



    }

    
}
