using System;
using System.Collections.Generic;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IShowData<T> where T: IComparable
    {
        void ShowData(List<T> data);
    }
}