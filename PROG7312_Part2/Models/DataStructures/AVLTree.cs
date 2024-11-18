using System;
using System.Collections.Generic;
using PROG7312_Part2.Pages;
/*using ServiceRequestStatus.Models;*/
using PROG7312_Part2.Models.DataStructures;


namespace PROG7312_Part2.Models.DataStructures
{
    public class AVLTree
    {
        private class Node
        {
            public ServiceRequest Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            public Node(ServiceRequest data)
            {
                Data = data;
                Left = Right = null;
                Height = 1;
            }
        }

        private Node _root;

        public void Insert(ServiceRequest data)
        {
            _root = Insert(_root, data);
        }

        private Node Insert(Node node, ServiceRequest data)
        {
            if (node == null) return new Node(data);

            if (data.ReqID < node.Data.ReqID)
                node.Left = Insert(node.Left, data);
            else if (data.ReqID > node.Data.ReqID)
                node.Right = Insert(node.Right, data);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && data.ReqID < node.Left.Data.ReqID)
                return RotateRight(node);
            if (balance < -1 && data.ReqID > node.Right.Data.ReqID)
                return RotateLeft(node);
            if (balance > 1 && data.ReqID > node.Left.Data.ReqID)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && data.ReqID < node.Right.Data.ReqID)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private int Height(Node node) => node == null ? 0 : node.Height;

        private int GetBalance(Node node) =>
            node == null ? 0 : Height(node.Left) - Height(node.Right);

        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public ServiceRequest Search(int id)
        {
            return Search(_root, id)?.Data;
        }

        private Node Search(Node node, int id)
        {
            if (node == null || node.Data.ReqID == id) return node;

            return id < node.Data.ReqID
                ? Search(node.Left, id)
                : Search(node.Right, id);
        }

        public List<ServiceRequest> InOrderTraversal()
        {
            var result = new List<ServiceRequest>();
            InOrder(_root, result);
            return result;
        }

        private void InOrder(Node node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrder(node.Left, result);
                result.Add(node.Data);
                InOrder(node.Right, result);
            }
        }
    }
}

