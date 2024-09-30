using OnlineStore.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Order> GetReviewsByUser(string userid);
        ReviewsByProductVariant GetReviewByProductVariantId(int productVariantId);
        int AddReview(AddReview review,int prodcutVariant);
        AddReview UpdateReview(AddReview review);
        AddReview DeleteReview(int reviewId, string userID);
    }
}
