using Core.Models;

public interface IPaymentService
{
    PaymentResponse ProcessPayment(PaymentRequest request);
}
