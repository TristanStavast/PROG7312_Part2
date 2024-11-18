namespace PROG7312_Part2.Models
{
    public class BST
    {
        public class Node
        {
            public ServiceRequest Request { get; set; }
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

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private Node InsertRec(Node root, ServiceRequest request)
        {
            if (root == null)
            {
                root = new Node(request);
                Console.WriteLine($"Inserted: {request.Id} - {request.Description}");
                return root;
            }

            if (request.Id < root.Request.Id)
                root.Left = InsertRec(root.Left, request);
            else if (request.Id > root.Request.Id)
                root.Right = InsertRec(root.Right, request);

            return root;
        }

        public ServiceRequest Search(int id)
        {
            return SearchRec(root, id);
        }

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

            if (id < root.Request.Id)
            {
                return SearchRec(root.Left, id);
            }

            return SearchRec(root.Right, id);
        }

        public void PrintBST()
        {
            Console.WriteLine("BST CONTENTS:");
            PrintRec(root);
        }
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
