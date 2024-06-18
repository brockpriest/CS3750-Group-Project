using Assignment13750.Data;
using Assignment13750.Models;
using System.Linq;

namespace StripeExample.Data
{
    public class PaymentStatusRepository : IPaymentStatusManager
    {
        private readonly Assignment13750.Data.Assignment13750Context _context;

        public PaymentStatusRepository(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }
        public void Add(PaymentStatus payment)
        {
            _context.PaymentStatus.Add(payment);
            _context.SaveChanges();
        }

        public bool DoesExist(string paymentToken)
        {
            PaymentStatus Test = _context.PaymentStatus.FirstOrDefault(x => x.PaymentToken == paymentToken); 
            if (Test == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
