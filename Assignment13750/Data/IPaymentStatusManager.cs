using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface IPaymentStatusManager
    {
        public void Add(PaymentStatus payment);
        public bool DoesExist(string PaymentToken);

    }
}
