using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;
using System.IO;
using Newtonsoft.Json;

namespace Assignment13750.Pages.Payment
{
    public class SuccessModel : PageModel
    {
        private ICredentialManager credrepo;
		private IPaymentStatusManager statusmanager;
		
		public SuccessModel(ICredentialManager credentialManager, IPaymentStatusManager paymentStatusManager)
		{
			credrepo = credentialManager;
			statusmanager = paymentStatusManager;
		}
		[FromRoute]
		public string? Id { get; set; }
		public Credentials credentials { get; set; }
		public bool FailedPayment = false;
		public void OnGet()
        {
            StripeConfiguration.ApiKey = "sk_test_51O0StDLG4yxZhbAz4Su1d4vP3oTMLHTdVbDPgJv49yA137Oqiza89ZA13I8c8S39AWDt6MsgAPOCP65O8WtRW0pA00oP8xQBDD";
            var service = new SessionService();
            Session session = service.Get(Id.ToString());
            credentials = credrepo.GetCredById(Int32.Parse(User.Identity.Name));
			if (statusmanager.DoesExist(Id)) // means they've already been credited
			{
				return;
			}
			else
			{
				if (session.PaymentStatus == "paid")
				{
					PaymentStatus newstatus = new PaymentStatus();
					newstatus.UserID = credentials.ID;
					newstatus.PaymentToken = Id.ToString();
                    credentials.TuitionBalance = credentials.TuitionBalance - (int)(session.AmountTotal / 100);
                    credrepo.Update(credentials);  //updates tuition balance
                    statusmanager.Add(newstatus);
				}
				else if (session.PaymentStatus == "unpaid")
				{
					FailedPayment = true;
				}
			}
			
			
		}
		
	}
}
