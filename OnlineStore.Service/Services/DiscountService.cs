using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class DiscountService: IDiscountService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Discount> _discounts;
        private IRepository<Product> _products;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _discounts = _unitOfWork.Repository<Discount>();
            _products = _unitOfWork.Repository<Product>();
        }

        public IEnumerable<Discount> GetAllDiscounts()
        {
            return _discounts.GetAll();
        }

        public Discount GetDiscountById(int id)
        {
            return _discounts.GetById(id) ?? throw new ArgumentException($"Discount with ID {id} not found.");
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
            _unitOfWork.Commit();
        }

        public void UpdateDiscount(Discount discount)
        {
            _discounts.Update(discount);
            _unitOfWork.Commit();
        }

        public void DeleteDiscount(int id)
        {
            var discount = _discounts.GetById(id);
            if (discount == null)
            {
                throw new ArgumentException($"Discount with ID {id} not found.");
            }
            _discounts.Delete(id);
            _unitOfWork.Commit();
        }

        public void ApplyDiscountToProduct(int productId, int discountId)
        {
            var product = _products.GetById(productId);
            var discount = _discounts.GetById(discountId);

            if (product == null)
            {
                throw new ArgumentException($"Product with ID {productId} not found.");
            }

            if (discount == null)
            {
                throw new ArgumentException($"Discount with ID {discountId} not found.");
            }

            product.DiscountId = discountId;
            product.Discount = discount;
            _products.Update(product);
            _unitOfWork.Commit();
        }
    }
}
