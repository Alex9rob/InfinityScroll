using System;
using System.Collections.Generic;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IHotStorage<T> where T : IComparable
    {
        List<T> Get();
        void Add(IEnumerable<T> items);
        void Clear();
    }
}