using Stripe;
using Stripe.Checkout;

namespace ZimFund.Services
{
    public class StripePaymentService
    {
        private readonly ILogger<StripePaymentService> _logger;

        public StripePaymentService(ILogger<StripePaymentService> logger)
        {
            _logger = logger;
        }

        public async Task<string> CreateCheckoutSessionAsync(decimal amount, string projectName, string successUrl, string cancelUrl)
        {
            try
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
                                Currency = "aoa",
                                UnitAmount = (long)(amount * 100),
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
                return session.Url;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de conexão com o Stripe.");
                throw new ApplicationException("Não foi possível conectar-se ao serviço de pagamento. Verifique sua conexão com a internet.");
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Erro do Stripe: {Message}", ex.Message);
                throw new ApplicationException("Ocorreu um erro no serviço de pagamento. Tente novamente mais tarde.");
            }
        }
    }
}
