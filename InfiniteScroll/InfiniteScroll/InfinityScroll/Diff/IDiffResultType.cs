using System.Collections.Generic;

namespace InfiniteScroll.InfinityScroll.Diff
{
    public interface IDiffResultType
    {
        HashSet<int> Inserts { get; set; }
        HashSet<int> Updates { get; set; }
        HashSet<int> Deletes { get; set; }
        List<MoveIndex> Moves { get; set; }
        bool HasChanges { get; }
    }
}