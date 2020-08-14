using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Blue : IBlue
    {
        private List<int> _data;

        public Blue()
        {
            _data = new List<int>();
        }

        public List<int> Get()
        {
            Console.WriteLine("BlueDataCount " + _data.Count);

            return _data.ToList();
        }

        public void Add(List<int> items)
        {
            _data.AddRange(items);
        }

        public void Clear()
        {
            _data = new List<int>();
        }
    }
}