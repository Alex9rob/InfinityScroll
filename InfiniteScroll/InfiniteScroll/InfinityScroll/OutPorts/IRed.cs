using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IRed
    {
        Task<List<int>> GetFrom(int item);
        void AddFrom(int item, List<int> items);
        void Clear();
    }
}