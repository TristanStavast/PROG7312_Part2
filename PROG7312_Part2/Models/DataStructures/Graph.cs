using PROG7312_Part2.Models.DataStructures;
using System.Collections.Generic;


namespace PROG7312_Part2.Models.DataStructures
{
    public class Graph
    {
        private readonly Dictionary<int, List<int>> _adjacenctList;

        public Graph()
        {
            _adjacenctList = new Dictionary<int, List<int>>();
        }

        public void AddEdge(int from, int to)
        {
            if(!_adjacenctList.ContainsKey(from))
            {
                _adjacenctList[from] = new List<int>();                
            }
            _adjacenctList[from].Add(to);
        }

        public List<int> GetDependencies(int id)
        {
            return _adjacenctList.ContainsKey(id) ? _adjacenctList[id] : new List<int>();
        }
    }
}
