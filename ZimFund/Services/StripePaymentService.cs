using Stripe.Checkout;

namespace ZimFund.Services
{
    public class StripePaymentService
    {
        public async Task<string> CreateCheckoutSessionAsync(decimal amount, string projectName, string successUrl, string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(amount * 100), // Stripe trabalha em centavos
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = projectName,
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url; // Este será o link para redirecionar o usuário
        }
    }
}
