using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IRed
    {
        bool IsBorder(int item, EDirection direction);
        Task<List<int>> GetFrom(int item, EDirection direction);
        void RemoveFrom(int item, EDirection direction);
        void AddFrom(int item, List<int> items);
    }
}