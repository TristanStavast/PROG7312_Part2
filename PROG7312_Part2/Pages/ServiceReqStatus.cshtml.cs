using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG7312_Part2.Models;

namespace PROG7312_Part2.Pages
{
    public class ServiceReqStatusModel : PageModel
    {
        // A static Binary Search Tree (BST) to hold service requests.
        private static BST requestBST = new BST();
        private MinHeap requestHeap = new MinHeap();

        // Flag to check if the BST has already been initialized.
        private static bool isBSTInitialized = false;

        // List to hold all the service requests for display.
        public List<ServiceRequest> requests { get; set; } = new List<ServiceRequest>();
        // Message to show user feedback 
        public string Message { get; set; }

        // This method handles the page load (GET request).
        public void OnGet()
        {
            // Initialize the requests only once by checking if the BST has been initialized.
            if (!isBSTInitialized)
            {
                // Populate the requests list with sample data.
                InitializeRequests();

                // Insert all requests into the BST and MinHeap for later use.
                foreach (var request in requests)
                {
                    requestBST.Insert(request);
                    requestHeap.Insert(request);
                }

                // Set the flag to prevent re-initialization of the BST.
                isBSTInitialized = true;
            }
            requestBST.PrintBST();
        }

        // This method handles the search functionality (POST request).
        public IActionResult OnPostSearch(int requestId)
        {
            // Check if the BST is available; if not, return an error message.
            if (requestBST == null)
            {
                // Set error message when the BST is not available.
                Message = "Service request list is not available";
                requests.Clear();
                return Page();
            }

            // Search the BST for a request with the specified ID.
            var request = requestBST.Search(requestId);

            // If a request is found, set a success message and update the requests list.
            if (request != null)
            {
                // Display the request details in the message.
                Message = $"Request ID: {requestId}: {request.Description} - {request.Status}";
                requests = new List<ServiceRequest> { request }; 
            }
            else
            {
                // If no request is found, inform the user.
                Message = $"Request ID {requestId} was not found!";
            }

            // Log the search result to the console (useful for debugging).
            foreach (var req in requests)
            {
                Console.WriteLine($"ID: {req.Id}, Description: {req.Description}, Status: {req.Status}");
            }
            return Page();
        }

        // This method handles refreshing the list of requests (POST request).
        public IActionResult OnPostRefresh()
        {
            // Reset the BST and initialize requests again.
            requestBST = new BST();
            InitializeRequests();

            // Re-insert all requests into the BST (and MinHeap, though not used here).
            foreach (var request in requests)
            {
                requestBST.Insert(request);
            }

            // Set a message to inform the user that the list has been refreshed.
            Message = "Requests have been refreshed!";
            return Page();
        }

        // Helper method to initialize and populate the requests list.
        private void InitializeRequests()
        {
            #region Adding Service Requests
            // Add various sample service requests with different statuses.
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

        // Helper method to check if the BST is empty (used for error handling).
        private bool IsBSTEmpty()
        {
            return requestBST.Search(int.MaxValue) == null; 
        }
    }
}
