namespace PROG7312_Part2.Models
{
    public class BST
    {
        // Node class representing each element in the BST
        public class Node
        {
            public ServiceRequest Request { get; set; } // ServiceRequest associated with the node
            public Node Left { get; set; }
            public Node Right { get; set; } 

            public Node(ServiceRequest request)
            {
                Request = request;
                Left = Right = null;
            }
        }

        private Node root; 

        public BST()
        {
            root = null; 
        }

        // Inserts a new request into the BST
        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        // Recursive helper method to insert a new request into the tree
        private Node InsertRec(Node root, ServiceRequest request)
        {
            if (root == null) 
            {
                root = new Node(request);
                return root;
            }

            if (request.Id < root.Request.Id) // Insert to the left if the request ID is smaller
                root.Left = InsertRec(root.Left, request);
            else if (request.Id > root.Request.Id) // Insert to the right if the request ID is larger
                root.Right = InsertRec(root.Right, request);

            return root; 
        }

        // Searches for a service request by ID
        public ServiceRequest Search(int id)
        {
            return SearchRec(root, id);
        }

        // Recursive helper method to search for a service request by ID
        private ServiceRequest SearchRec(Node root, int id)
        {
            if (root == null) 
            {
                Console.WriteLine($"Search for ID {id}: Not Found");
                return null;
            }

            if (root.Request.Id == id) 
            {
                Console.WriteLine($"Search for ID {id}: Found {root.Request.Description}");
                return root.Request;
            }

            if (id < root.Request.Id) // Search left if ID is smaller
                return SearchRec(root.Left, id);

            return SearchRec(root.Right, id); // Search right if ID is larger
        }

        // Prints the contents of the BST in-order (left, root, right)
        public void PrintBST()
        {
            Console.WriteLine("BST CONTENTS:");
            PrintRec(root);
        }

        // Recursive helper method to print the BST in-order
        private void PrintRec(Node root)
        {
            if (root != null)
            {
                PrintRec(root.Left); 
                Console.WriteLine($"Request ID: {root.Request.Id}, Description: {root.Request.Description}, Status: {root.Request.Status}"); 
                PrintRec(root.Right); 
            }
        }
    }
}
