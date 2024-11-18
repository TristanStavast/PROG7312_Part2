# Municipality Progressive Web Application

## How to Compile, Run and Use the Application
``git clone https://github.com/TristanStavast/PROG7312_Part2``


## Install Dependencies
1. Install Visual Studio if it is not already installed.
2. Ensure you have .NET core SDK installed.
3. Open the project folder in Visual Studio.


## Build and Run the Project
1. Locate the Run button somewhere near the top middle of the screen and click on it.
2. The Application will then run on http://localhost:5000 in your browser of choice.


## Usage
1. Part 1: Report Issues
   - Users can report issues under 4 categories: Roads, Utilities, Water and Other.
   - Each report requires the user to select a category, enter a location and add an optional file attachment.
   - Reports are stored in memory and persist during a session. A progress bar was implemented in order to show user engagement while submitting reports.

2. Part 2: Local Events and Announcements
   - Users can search for events by category: Sports, Education, Art and Music as well as search by date along with a category.
   - The upcoming events are displayed by date using LINQ, and the events are color-coded based on their category.

3. Part 3: Service Request Status
   - Users can view the status of service requests: Pending, Under Review, In Progress and Completed as well as search for specific requests by a unique ID.
   - The status of each request is displayed with a progress bar to provide a visual representation of its progress.


## Data Structure Uses
Each of the following data structures were used in this project.

1. Binary Search Tree (BST)
     - The Binary Search Tree was used to store and organize service requests based on their ID. This data structure enables efficient searching, insertion, and deletion of service requests.
     - The contribution to efficiency of the BST is the search efficiency. Searching for a specific service request by its ID is fast with a BST because it uses a divide-and-conquer method, resulting in O(log n) search time in a balanced tree.
     - Example: When a user searches for a specific service request, the BST ensures that the request is found quickly, even if there are many requests in the system.

2. MinHeap
   - The MinHeap was used to manage service requests based on priority. The service request with the highest priority (i.e., the smallest value) is processed first.
   - The contribution to efficiency of the MinHeap was to ensure that the service requests with the highest priority are processed first. It maintains the heap property, where the minimum element is always at the root, and allows efficient extraction of the minimum element (O(log n) complexity).
   - Example: When a user views the status of multiple requests, the requests are processed in priority order, ensuring that urgent service requests are dealt with before others.

3. Queue
   - The Queue was used to manage the "Upcoming Events" list. This ensures that events are handled in the order they are added (First-In-First-Out - FIFO).
   - A Queue allows for the processing of service requests or events in a predictable order, ensuring that each request or event is handled in the sequence it was received.
   - Example: When viewing upcoming events, the events are displayed in the order they were created, and the Queue ensures that the next event is always retrieved in the correct sequence.

4. HashSet
   - The HashSet was used to ensure that each event date is unique and that duplicate dates are not displayed in the local events search results.
   - A HashSet does not allow for users to add or checking for duplicates, making it an ideal structure to ensure that event dates are unique and avoid redundant entries.
   - Example: When searching for local events, the HashSet ensures that only unique event dates are shown, avoiding the display of events scheduled on the same date multiple times.

5. Sorted Dictionary
   - The SortedDictionary was used to store service requests by category or status. It provides an easy way to maintain and search for service requests based on these criteria.
   - A SortedDictionary automatically keeps the service requests sorted based on their keys (such as category or status), allowing for quick access and searching
   - Example: When users search for service requests by status (e.g., Pending or Completed), the SortedDictionary ensures that the requests are quickly retrieved in sorted order, making the user experience smoother and faster.
