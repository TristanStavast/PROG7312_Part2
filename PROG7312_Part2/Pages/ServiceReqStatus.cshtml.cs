using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using PROG7312_Part2.Models;

namespace PROG7312_Part2.Pages
{
    public class ServiceReqStatusModel : PageModel
    {
        private static BST requestBST = new BST();
        private MinHeap requestHeap = new MinHeap();

        private static bool isBSTInitialized = false;

        public List<ServiceRequest> requests { get; set; } = new List<ServiceRequest>();
        public string Message { get; set; }

        public void OnGet()
        {
            Console.WriteLine("ON GET TRIGGERED!!");

            if (!isBSTInitialized)
            {
                Console.WriteLine("Initializing BST");
                InitializeRequests();

                foreach(var request in requests) 
                {
                    requestBST.Insert(request);
                    requestHeap.Insert(request);
                }

                isBSTInitialized = true;
                Console.WriteLine("BST Initialization Complete.");

            }

            requestBST.PrintBST();
        }

        public IActionResult OnPostSearch(int requestId)
        {
            Console.WriteLine($"OnPostSearch triggered with Req ID: {requestId}");
            var testRequest = requestBST.Search(1);
            Console.WriteLine(testRequest);

            if (requestBST == null)
            {
                Console.WriteLine("BST is not initialized!");
                Message = "Service request list is not available";
                requests.Clear();

                return Page();
            }

            var request = requestBST.Search(requestId);

            if (request != null)
            {
                Console.WriteLine($"Found test request: {testRequest.Description}");
                Message = $"Request ID: {requestId}: {request.Description} - {request.Status}";
                requests = new List<ServiceRequest> { request };

                Console.WriteLine($"Found Request: ID: {request.Id}, Description: {request.Description}, Status: {request.Status}");
            }
            else
            {
                Message = $"Request ID {requestId} was not found!";
                //requests.Clear();

                Console.WriteLine($"No request found with ID: {requestId}");
            }

            Console.WriteLine("Requests after search");
            foreach(var req in requests)
            {
                Console.WriteLine($"ID: {req.Id}, Description: {req.Description}, Status: {req.Status}");
            }

            return Page();
        }

        public IActionResult OnPostRefresh()
        {
            requestBST = new BST();
            InitializeRequests();

            foreach(var request in requests)
            {
                requestBST.Insert(request);
            }

            Message = "Requests have been refreshed!";
            Console.WriteLine("Requests refreshed and reinitialized.");

            return Page();
        }

        private void InitializeRequests()
        {
            #region Adding Service Requests
            requests.Add(new ServiceRequest { Id = 1, Description = "Water Leakage", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 2, Description = "Power Outage", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 3, Description = "Road Repair", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 4, Description = "Street Light Repair", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 5, Description = "Sewer Line Blockage", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 6, Description = "Pothole Filling", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 7, Description = "Broken Water Pipe", Status = "Under Review" });
            requests.Add(new ServiceRequest { Id = 8, Description = "Stormwater Drain Cleaning", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 9, Description = "Public Toilet Maintenance", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 10, Description = "Electricity Cable Repair", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 11, Description = "Tree Branch Removal", Status = "Under Review" });
            requests.Add(new ServiceRequest { Id = 12, Description = "Park Bench Replacement", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 13, Description = "Broken Sidewalk Repair", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 14, Description = "Traffic Signal Repair", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 15, Description = "Public Wi-Fi Installation", Status = "Under Review" });
            requests.Add(new ServiceRequest { Id = 16, Description = "Parking Lot Repainting", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 17, Description = "Library Facility Repair", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 18, Description = "Bus Stop Shelter Repair", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 19, Description = "Road Sign Replacement", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 20, Description = "Playground Equipment Maintenance", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 21, Description = "Air Conditioning Repair", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 22, Description = "Building Roof Inspection", Status = "Under Review" });
            requests.Add(new ServiceRequest { Id = 23, Description = "Flood Control System Inspection", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 24, Description = "Sidewalk Maintenance", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 25, Description = "Public Fountain Repair", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 26, Description = "Electric Meter Replacement", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 27, Description = "Water Treatment Plant Maintenance", Status = "Pending" });
            requests.Add(new ServiceRequest { Id = 28, Description = "Street Sweeping Request", Status = "In Progress" });
            requests.Add(new ServiceRequest { Id = 29, Description = "Road Surface Resurfacing", Status = "Completed" });
            requests.Add(new ServiceRequest { Id = 30, Description = "Street Tree Planting", Status = "Under Review" });
            #endregion
        }

        private bool IsBSTEmpty()
        {
            return requestBST.Search(int.MaxValue) == null;
        }
    }
}
