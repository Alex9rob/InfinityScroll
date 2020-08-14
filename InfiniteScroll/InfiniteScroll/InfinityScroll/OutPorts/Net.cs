using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Net : INet
    {
        public Task<List<int>> GetFrom(int item)
        {
            throw new System.NotImplementedException();
        }
    }
}