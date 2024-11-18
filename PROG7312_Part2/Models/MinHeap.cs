namespace PROG7312_Part2.Models
{
    public class MinHeap
    {
        // List to store heap elements
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        // Inserts a new request into the heap and re-adjusts the heap
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(); // Adjust the heap to maintain the min-heap property
        }

        // Moves the newly added element up to its correct position
        private void HeapifyUp()
        {
            int index = heap.Count - 1;
            while (index > 0 && heap[index].Id < heap[(index - 1) / 2].Id)
            {
                var temp = heap[index];
                heap[index] = heap[(index - 1) / 2];
                heap[(index - 1) / 2] = temp;
                index = (index - 1) / 2;
            }
        }

        // Extracts the smallest element (root) and re-adjusts the heap
        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0)
                return null;

            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(); // Adjust the heap after removal
            return min;
        }

        // Moves the root element down to its correct position
        private void HeapifyDown()
        {
            int index = 0;
            int leftChild, rightChild, smallest;

            while (index < heap.Count / 2)
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;
                smallest = index;

                if (leftChild < heap.Count && heap[leftChild].Id < heap[smallest].Id)
                    smallest = leftChild;

                if (rightChild < heap.Count && heap[rightChild].Id < heap[smallest].Id)
                    smallest = rightChild;

                if (smallest == index)
                    break;

                var temp = heap[index];
                heap[index] = heap[smallest];
                heap[smallest] = temp;

                index = smallest;
            }
        }
    }
}
