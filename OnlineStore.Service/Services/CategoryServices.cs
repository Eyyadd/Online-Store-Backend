using AutoMapper;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private IUnitOfWork _unitOfWork;
        private ICategoryRepository Categories;
        private readonly IMapper _mapper;

        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Categories = _unitOfWork.CategoryRepository();
            _mapper = mapper;
        }

        public int AddCategory(CategoriesDTO category)
        {
            var IsExist = Categories.GetByNameAndType(category.Name, category.CategoryType);
            int EffectedRow = -1;
            if (IsExist is null)
            {
                var NewCategory = _mapper.Map<Category>(category);
                Categories.Add(NewCategory);
                EffectedRow = _unitOfWork.Commit();
            }
            return EffectedRow;
        }

        public IEnumerable<CategoriesDTO> GetCategories()
        {
            var AllCategories = _mapper.Map<IEnumerable<CategoriesDTO>>(Categories.GetAll());
            return AllCategories;
        }

        public CategoriesDTO GetCategory(int id)
        {
            var Category = _mapper.Map<CategoriesDTO>(Categories.GetById(id));
            return Category;
        }

        public int RemoveCategory(int id)
        {
            var CheckCategory = Categories.GetById(id);
            var DeleteResult = -1;
            if (CheckCategory is not null)
            {
                Categories.Delete(id);
                DeleteResult = _unitOfWork.Commit();
            }
            return DeleteResult;

        }

        //TODO Delete ID
        public int UpdateCategory(UpdatedCategoryDTO category)
        {
            var IsExistCategory = Categories.GetByIdWithNoTracking(category.Id);
            var UpdateResult = -1;

            if (IsExistCategory is not null)
            {
                IsExistCategory = _mapper.Map<UpdatedCategoryDTO, Category>(category);
                Categories.Update(IsExistCategory);
                UpdateResult = _unitOfWork.Commit();
            }

            return UpdateResult;

        }
    }
}
