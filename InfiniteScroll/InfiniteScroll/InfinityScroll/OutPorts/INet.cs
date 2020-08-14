using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface INet
    {
        Task<List<int>> GetFrom(int item);
    }
}