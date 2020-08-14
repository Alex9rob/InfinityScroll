using System;
using System.Collections.Generic;
using System.Linq;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class HotStorage : IHotStorage
    {
        private List<Number> _data;

        public HotStorage()
        {
            _data = new List<Number>();
        }

        public List<Number> Get()
        {
            Console.WriteLine("BlueDataCount " + _data.Count);

            return _data.ToList();
        }

        public void Add(List<Number> items)
        {
            _data.AddRange(items);
        }

        public void Clear()
        {
            _data = new List<Number>();
        }
    }
}