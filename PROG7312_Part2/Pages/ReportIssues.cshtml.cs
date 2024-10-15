using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG7312_Part2.Pages.Shared;

namespace PROG7312_Part2.Pages
{
    public class ReportIssuesModel : PageModel
    {
        //Binding all properties
        [BindProperty]
        public Report report { get; set; } = new Report();

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        //Dictionary for data
        public static Dictionary<DateTime, Report> Reports = new Dictionary<DateTime, Report>();

        public void OnGet()
        { }

        //Async method to return page if there is an error
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Model Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            string filePath = string.Empty;

            if (UploadedFile != null && UploadedFile.Length > 0)
            {
                var fileName = Path.GetFileName(UploadedFile.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }
            }

            var newReport = new Report
            {
                Location = report.Location,
                Category = report.Category,
                Description = report.Description,
                ImagePath = filePath
            };

            Reports.Add(DateTime.Now, newReport);

            Console.WriteLine($"Total Reports: {Reports.Count}");

            ModelState.Clear();
            return RedirectToPage();
        }
    }
}

