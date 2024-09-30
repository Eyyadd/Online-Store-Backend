using AutoMapper;
using OnlineStore.Application.DTOs.Discount;
using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Discount> _discounts;
        private IRepository<Product> _products;
        private readonly IMapper _Mapper;

        public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _discounts = _unitOfWork.Repository<Discount>();
            _products = _unitOfWork.Repository<Product>();
            _Mapper = mapper;
        }

        public IEnumerable<DiscountDTO> GetAllDiscounts()
        {
            var AllDiscounts = _Mapper.Map<IEnumerable<Discount>, IEnumerable<DiscountDTO>>(_discounts.GetAll());

            return AllDiscounts;
        }

        public DiscountDTO GetDiscountById(int id)
        {
            var Disount = _Mapper.Map<Discount, DiscountDTO>(_discounts.GetById(id));
            return Disount;
        }

        public int AddDiscount(AddDiscountDTO discount)
        {
            var checkDiscount = _discounts.GetByNameWithNoTracking(discount.Name);
            var AddedResult = -1;
            if (checkDiscount is null)
            {
                var MappedDiscount = _Mapper.Map<AddDiscountDTO, Discount>(discount);
                _discounts.Add(MappedDiscount);
                AddedResult = _unitOfWork.Commit();

            }
            return AddedResult;
        }

        public int UpdateDiscount(DiscountDTO discount)
        {
            var OldDiscount = _discounts.GetByIdWithNoTracking(discount.Id);
            int UpdateResult = -1;
            if (OldDiscount is not null)
            {
                var NewDiscount = _Mapper.Map<DiscountDTO, Discount>(discount);
                _discounts.Update(NewDiscount);
                UpdateResult = _unitOfWork.Commit();
            }
            return UpdateResult;
        }

        public int DeleteDiscount(int id)
        {
            var discount = _discounts.GetById(id);
            var DeleteResult = -1;
            if (discount is not null)
            {
                _discounts.Delete(id);
                DeleteResult = _unitOfWork.Commit();
                return DeleteResult;
            }
            return DeleteResult;
        }

        public int ApplyDiscountToProduct(int productId, int discountId)
        {
            var product = _products.GetById(productId);
            var discount = _discounts.GetById(discountId);
            var AppliedDiscountResult = -1;
            if (product is not null && discount is not null)
            {
                product.DiscountId = discountId;
                product.Discount = discount;
                product.Discounted = true;
                _products.Update(product);
                AppliedDiscountResult = _unitOfWork.Commit();
            }
            return AppliedDiscountResult;
        }
    }
}
