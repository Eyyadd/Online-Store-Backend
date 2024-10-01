using Microsoft.Extensions.Configuration;
using OnlineStore.Application.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductVariant = OnlineStore.Domain.Entities.ProductVariants;

namespace OnlineStore.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICartRepository _CartRepository;
        private readonly IUnitOfWork _UnitOfWork;

        public PaymentService(IConfiguration configuration, ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _CartRepository = cartRepository;
            _UnitOfWork = unitOfWork;
        }



        public Cart CreatePaymentIntent(int CartID)
        {
            StripeConfiguration.ApiKey = _configuration["stripe:Secretkey"];

            var myCart = _CartRepository.GetCartByIdWithInclude(CartID);
            var ShipmentCost = 50;
            if (myCart is null) return null;
            if (myCart.Items?.Count > 0)
            {
                foreach (var item in myCart.Items)
                {
                    var product = _UnitOfWork.ProductRepository().GetProductVariantByIdWithInclude(item.ProductID);
                    if (item.ProductVariants.Product.Price != product.Product.Price)
                    {
                        item.ProductVariants.Product.Price = product.Product.Price;
                    }
                }
            }
            myCart.ShippingCost = ShipmentCost;

            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrEmpty(myCart.PaymentIntentId))
            {
                var PaymentOption = new PaymentIntentCreateOptions()
                {
                    Amount = (long)myCart.Items.Sum(c => c.ProductVariants.Product.Price * 100 * c.Qunatity) + (long)ShipmentCost * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = paymentIntentService.Create(PaymentOption);
                myCart.PaymentIntentId = paymentIntent.Id;
                myCart.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var PaymentOption = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)myCart.Items.Sum(c => c.ProductVariants.Product.Price * 100 * c.Qunatity) + (long)ShipmentCost * 100,

                };
                paymentIntentService.Update(myCart.PaymentIntentId,PaymentOption);
            }
            _CartRepository.Update(myCart);
            _UnitOfWork.Commit();
            return myCart;
        }
    }
}
