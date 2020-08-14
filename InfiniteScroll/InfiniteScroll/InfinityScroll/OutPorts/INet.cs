using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface INet
    {
        Task<List<Number>> GetFrom(int item);
    }
}