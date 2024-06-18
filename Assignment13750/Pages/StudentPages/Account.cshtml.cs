using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using System.Threading.Tasks;
using Assignment13750.Models;
using Assignment13750.Data;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Operations;

namespace Assignment13750.Pages.StudentPages
{
	public class AccountModel : PageModel
	{

		private ICredentialManager credrepo;

		public AccountModel(ICredentialManager credentialManager, ILogger<AccountModel> logger)
		{
			credrepo = credentialManager;

		}
	

		[BindProperty]
		public int PaymentAmount { get; set; }
		public Credentials TuitionUser { get; set; }
		public Session session = new Session();

		public void OnGet()
		{

			TuitionUser = credrepo.GetCredById(Int32.Parse(User.Identity.Name));
		}
		private string SessionID;
		public async Task<IActionResult> OnPostAsync()
		{
			StripeConfiguration.ApiKey = "sk_test_51O0StDLG4yxZhbAz4Su1d4vP3oTMLHTdVbDPgJv49yA137Oqiza89ZA13I8c8S39AWDt6MsgAPOCP65O8WtRW0pA00oP8xQBDD";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = PaymentAmount*100,
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = "Tuition",
							},
						},
						Quantity = 1,
					},
				},
				Mode = "payment",
				SuccessUrl = "https://localhost:7236/Payment/success/{CHECKOUT_SESSION_ID}",
				
				CancelUrl = "https://localhost:7236/cancel",

			};

			var service = new SessionService();
			session = service.Create(options);
			
			SessionID = session.Id;
			
		
			Response.Headers.Add("Location", session.Url);

			return new StatusCodeResult(303);
		}
		


		



	}
}
