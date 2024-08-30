using Core.Models;

public class MockPayPalPaymentService : IPaymentService
{
    public PaymentResponse ProcessPayment(PaymentRequest request)
    {
        // Simulate processing payment
        return new PaymentResponse
        {
            Success = true,
            Message = "Payment processed successfully",
            TransactionId = Guid.NewGuid().ToString()
        };
    }
}
