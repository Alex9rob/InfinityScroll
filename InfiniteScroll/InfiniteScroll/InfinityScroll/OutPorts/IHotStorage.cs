using System.Collections.Generic;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IHotStorage
    {
        List<Number> Get();
        void Add(List<Number> items);
        void Clear();
    }
}