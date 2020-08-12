using System.Collections.Generic;
using System.Linq;

namespace InfiniteScroll.InfinityScroll.Diff
{
    public class DiffResult<H> : IDiffResultType
    {
        public HashSet<int> Inserts { get; set; }
        public HashSet<int> Updates { get; set; }
        public HashSet<int> Deletes { get; set; }
        public List<MoveIndex> Moves { get; set; }
        public Dictionary<H, int> OldMap { get; set; }
        public Dictionary<H, int> NewMap { get; set; }
        
        public DiffResult()
        {
            Inserts = new HashSet<int>();
            Updates = new HashSet<int>();
            Deletes = new HashSet<int>();
            Moves = new List<MoveIndex>();
            OldMap = new Dictionary<H, int>();
            NewMap = new Dictionary<H, int>();
        }

        public bool Validate<T, K>(List<T> oldArray, List<T> newArray) 
            where T : IDiffable<K>
        {
            return (oldArray.Count + Inserts.Count - Deletes.Count) == newArray.Count;
        }

        public int? OldIndexFor(H identifier)
        {
            return OldMap.TryGetValue(identifier, out var res) ? (int?) res : null;
        }
        
        public int? NewIndexFor(H identifier)
        {
            return NewMap.TryGetValue(identifier, out var res) ? (int?) res : null;
        }

        public override string ToString()
        {
            return string.Join("\n", 
                    $"Inserts => {string.Join(", ", Inserts)}",
                     $"Updates => {string.Join(", ", Updates)}",
                     $"Deletes => {string.Join(", ", Deletes)}",
                     $"Moves => {string.Join(", ", Moves)}"
            );
        }

        public bool HasChanges =>
            !IsNullOrEmpty(Inserts)
            || !IsNullOrEmpty(Deletes)
            || !IsNullOrEmpty(Updates)
            || !IsNullOrEmpty(Moves);

        public int ChangeCount =>
            Inserts.Count +
            Deletes.Count +
            Updates.Count +
            Moves.Count;

        private bool IsNullOrEmpty<M>(IEnumerable<M> enumerable)
        {
            switch (enumerable)
            {
                case null:
                    return true;
                case ICollection<M> collection:
                    return collection.Count == 0;
                default:
                    return !enumerable.Any();
            }
        }
    }
}