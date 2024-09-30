using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Interfaces;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(ICategoryServices categoryServices, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _categoryServices = categoryServices;
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        //[Authorize(AuthenticationSchemes ="Bearer")]
        public IActionResult GetAll()
        {
            var categories = _categoryServices.GetCategories();
            if (categories.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<CategoriesDTO>>(true, "Categories retrieved successfully", categories));
            }
            return Ok(new GeneralResponse<string>(false, "No categories available"));

        }

        [HttpGet("GetById/{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult GetById(int id)
        {

            var category = _categoryServices.GetCategory(id);
            if (category is not null)
            {
                return Ok(new GeneralResponse<CategoriesDTO>(true, "Category retrieved successfully", category));
            }
            return Ok(new GeneralResponse<string>(false, $"No category found with ID: {id}"));

        }

        [HttpPost("Add")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public ActionResult<GeneralResponse<CategoriesDTO>> Add(CategoriesDTO newCategory)
        {
            var Response = new GeneralResponse<CategoriesDTO>(false, "Not Added yet", null);
            if (ModelState.IsValid)
            {
                var AddResult = _categoryServices.AddCategory(newCategory);
                if (AddResult > 0)
                {
                    Response.Success = true;
                    Response.Message = $"Category - {newCategory.Name} - Added Successfully";
                    Response.Data = newCategory;

                }
                else
                {
                    Response.Success = false;
                    Response.Message = $"Can't Add this Category {newCategory.Name}";
                    Response.Data = newCategory;
                }
            }
            return Response;
        }

        [HttpPut("Update")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult Update(UpdatedCategoryDTO updatedCategory)
        {

            if (ModelState.IsValid)
            {
                var UpdateResult= _categoryServices.UpdateCategory(updatedCategory);
                if (UpdateResult > 0) 
                {
                     return Ok(new GeneralResponse<UpdatedCategoryDTO>(true, "Category updated successfully", updatedCategory));
                }
                return NotFound(new GeneralResponse<string>(false, $"No category found with ID: {updatedCategory.Id}"));
            }
            return BadRequest(new GeneralResponse<string>(false,"Wrong Category"));

        }

        [HttpDelete("Delete/{id:int}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            var DeleteCategory = _categoryServices.RemoveCategory(id);
            if (DeleteCategory > 0)
            {
                return Ok(new GeneralResponse<string>(true, "Category deleted successfully"));
            }
            return Ok(new GeneralResponse<string>(false, $"Failed to delete category. No category found with ID: {id}"));

        }
    }
}
