using System.Collections.Generic;

namespace InfiniteScroll.InfinityScroll.Diff
{
    public interface IDiffing
    {
        IDiffResultType Diffing<T, K>(List<T> oldList, List<T> newList, IEqualityComparer<T> isEqual) where T : IDiffable<K>;
    }
}