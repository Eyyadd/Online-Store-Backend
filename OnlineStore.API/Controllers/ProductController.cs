using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<GeneralResponse<List<ProductElementDTO>>> All()
        {
            //try
            //{
                var products = _productServices.AllProducts().ToList();

                if (products == null || products.Count == 0)
                {
                    var notFoundResponse = new GeneralResponse<List<ProductElementDTO>>(false, "No products found");
                    return NotFound(notFoundResponse);
                }

                var response = new GeneralResponse<List<ProductElementDTO>>(true, "Products retrieved successfully", products);
                return Ok(response);
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
            var ImagePathToRestor = await ImageUpload.UploadImageAsync(createProductDTO.ImageCover);
            var Result =  _productServices.CreateProduct(createProductDTO , ImagePathToRestor);
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


    }

    
}
