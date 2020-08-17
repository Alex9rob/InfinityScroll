using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class HotStorage<T> : IHotStorage<T> where T : IComparable
    {
        private List<T> _data;
        
        public HotStorage()
        {
            _data = new List<T>();
        }
        
        public List<T> Get()
        {
            return _data.ToList();
        }

        public void Add(IEnumerable<T> items)
        {
            _data.AddRange(items);
        }

        public void Clear()
        {
            _data = new List<T>();
        }
    }
}