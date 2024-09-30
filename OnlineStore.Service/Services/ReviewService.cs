using AutoMapper;
using OnlineStore.Application.DTOs.Review;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewService;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reviewService = _unitOfWork.ReviewRepository();
            _mapper = mapper;
        }

        public int AddReview(AddReview review, int prodcutVariant)
        {
            var AuthenticatedUser = review.UserId;
            var OrderList = _reviewService.GetOrdersIdByUserId(AuthenticatedUser);
            var OrderIdList = new List<int>();
            var AddResult = -1;
            foreach (var item in OrderList)
            {
                OrderIdList.Add(item.Id);
            }
            foreach (var OrderId in OrderIdList)
            {
                var CheckAddReveiw = _reviewService.CheckProdcutVariantAsOrder(prodcutVariant, OrderId);
                if (CheckAddReveiw)
                {
                    var mappedReview = _mapper.Map<AddReview, Review>(review);
                    _unitOfWork.ReviewRepository().Add(mappedReview);
                    AddResult = _unitOfWork.Commit();
                    return AddResult;
                }
            }
            return AddResult;

        }

        public AddReview DeleteReview(int reviewId, string userID)
        {
            var check = _reviewService.CheckUserAddReviewBefore(userID, reviewId);
            var DeleteREsult = -1;
            if (check)
            {
                var mappedReview = _reviewService.GetById(reviewId);
                var reviewDto = _mapper.Map<AddReview>(mappedReview);
                _reviewService.Delete(reviewId);
                DeleteREsult = _unitOfWork.Commit();
                if (DeleteREsult > 0)
                {
                    return reviewDto;
                }
            }
            return null;
        }

        public ReviewsByProductVariant GetReviewByProductVariantId(int productVariantId)
        {
            var AllReviewsByProduct = _reviewService.GetAllReviewsByProductVariantId(productVariantId);
            ReviewsByProductVariant Reviews = new ReviewsByProductVariant();
            Reviews.ProductId = productVariantId;
            foreach (var Rev in AllReviewsByProduct)
            {
                foreach (var Rv in Rev.Reviews)
                {
                    Reviews.Comment.Add(new CommentRateDTO
                    {
                        Comment = Rv.Comment is null ? "Null" : Rv.Comment,
                        Rate = Rv.Rate,
                    });
                }
            }
            return Reviews;
        }

        public IEnumerable<Order> GetReviewsByUser(string userid)
        {
            return _reviewService.GetAllReviewsByUserID(userid);
        }

        public AddReview UpdateReview(AddReview review)
        {
            var AuthenicatedUser = review.UserId;
            var UpdatedResult = -1;
            var check = _reviewService.CheckUserAddReviewBefore(AuthenicatedUser, review.Id);
            if (check)
            {
                var mappedReview = _mapper.Map<AddReview, Review>(review);
                _reviewService.Update(mappedReview);
                UpdatedResult = _unitOfWork.Commit();
                if (UpdatedResult > 0)
                {
                    return review;
                }
            }
            return null;

        }
    }
}
