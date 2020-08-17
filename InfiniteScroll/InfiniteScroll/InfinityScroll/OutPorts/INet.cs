using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface INet<T> where T : IComparable
    {
        Task<List<T>> Get();
        Task<List<T>> GetFrom(T item);
    }
}