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
            for (int i = 0; i < 100; i++)
            {
                _data.Add(new Number(i, Enumerables.EStorageType.Hot));
            }
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