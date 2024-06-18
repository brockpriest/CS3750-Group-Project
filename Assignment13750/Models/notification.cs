using Microsoft.AspNetCore.Mvc;

namespace Assignment13750.Models
{
    public class Notification : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int ResourceId { get; set; } // Reference to the related resource

    }
}