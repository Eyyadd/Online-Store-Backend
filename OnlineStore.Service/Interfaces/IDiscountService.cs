using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IDiscountService
    {
        IEnumerable<Discount> GetAllDiscounts();
        Discount GetDiscountById(int id);
        void AddDiscount(Discount discount);
        void UpdateDiscount(Discount discount);
        void DeleteDiscount(int id);

        // Method to apply a discount to a product
        void ApplyDiscountToProduct(int productId, int discountId);
    }
}
