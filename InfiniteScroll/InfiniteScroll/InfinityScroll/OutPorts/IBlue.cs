using System.Collections.Generic;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IBlue
    {
        bool IsBorder(int item, EDirection direction);
        List<int> Get();
        void RemoveFrom(int item, EDirection direction);
        void AddFrom(int item, List<int> items);
    }
}