using System.Collections.Generic;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IBlue
    {
        List<int> Get();
        void Add(List<int> items);
        void Clear();
    }
}