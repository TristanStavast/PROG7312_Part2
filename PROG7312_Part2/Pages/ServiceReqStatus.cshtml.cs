using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PROG7312_Part2.Pages
{
    public class ServiceReqStatusModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    public class ServiceRequest
    {
        public int ReqID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
