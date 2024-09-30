using OnlineStore.Application.DTOs.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IDiscountService
    {
        IEnumerable<DiscountDTO> GetAllDiscounts();
        DiscountDTO GetDiscountById(int id);
        int AddDiscount(AddDiscountDTO discount);
        int UpdateDiscount(DiscountDTO discount);
        int DeleteDiscount(int id);

        // Method to apply a discount to a product
        int ApplyDiscountToProduct(int productId, int discountId);
    }
}
