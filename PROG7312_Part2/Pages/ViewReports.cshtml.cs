using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG7312_Part2.Pages.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace PROG7312_Part2.Pages
{
    public class ViewReportsModel : PageModel
    {
        //Dictionary to store the data
        public Dictionary<DateTime, Report> Reports { get; set; } = new Dictionary<DateTime, Report>();

        public void OnGet()
        {
            Reports = ReportIssuesModel.Reports;

            // Ensure Reports is initialized
            if (Reports == null)
            {
                Reports = new Dictionary<DateTime, Report>();
            }
        }

        // Helper method to get the file name from the path
        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}
