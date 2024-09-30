using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IReviewRepository:IRepository<Review>
    {
        IEnumerable<Order> GetAllReviewsByUserID(string UserID);
        IEnumerable<ProductVariants> GetAllReviewsByProductVariantId(int ProductVariantId);
        // move it into orders 
        IEnumerable<Order> GetOrdersIdByUserId(string UserID);
        bool CheckProdcutVariantAsOrder(int productVariantId, int OrderId);
        bool CheckUserAddReviewBefore(string UserID, int ReviewID);
    }
}
