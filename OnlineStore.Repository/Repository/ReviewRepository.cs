using OnlineStore.Application;
using OnlineStore.Application.Repository;
using OnlineStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class ReviewRepository:Repository<Review>,IReviewRepository
    {
        public ReviewRepository(OnlineStoreDbContext onlineStoreDbContext) : base(onlineStoreDbContext) { }

        public bool CheckProdcutVariantAsOrder(int productVariantId, int OrderId)
        {
            var productInOrder=_context.OrderItems.Any(oi=>oi.orderId==OrderId&&oi.ProductId==productVariantId);
            return productInOrder;
        }

        public bool CheckUserAddReviewBefore(string UserID,int ReviewID)
        {
            var checkAddedReview=_context.Reviews.Any(rv=>rv.UserId==UserID&&rv.Id==ReviewID);
            return checkAddedReview;
        }

        public IEnumerable<ProductVariants> GetAllReviewsByProductVariantId(int ProductVariantId)
        {
            var productVariants=_context.ProductVariants.Include(r=>r.Reviews).Where(r=>r.Id == ProductVariantId);
            return productVariants;
        }

        public IEnumerable<Order> GetOrdersIdByUserId(string UserID)
        {
            var OrdersId = _context.Order.Where(o => o.UserId == UserID).ToList();
            return OrdersId;
        }

        IEnumerable<Order> IReviewRepository.GetAllReviewsByUserID(string UserID)
        {
           return _context.Order.Include(item => item.Items).Where(o => o.UserId == UserID);
        }
    }
}
