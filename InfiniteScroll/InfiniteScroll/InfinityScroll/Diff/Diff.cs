using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace InfiniteScroll.InfinityScroll.Diff
{
    public class Diff : IDiffing
    {
        private class Entry
        {
            private int OldCounter { get; set; }
            private int NewCounter { get; set; }
            public Stack<int?> OldIndexes { get; } = new Stack<int?>();
            public bool Updated { get; set; }
            public bool OccurOnBothSides => NewCounter > 0 && OldCounter > 0;

            public void PushNew(int? index)
            {
                NewCounter++;
                OldIndexes.Push(index);
            }

            public void PushOld(int? index)
            {
                OldCounter++;
                OldIndexes.Push(index);
            }
        }

        private class Record
        {
            public Record(Entry entry) 
            {
                Entry = entry;
                Index = null;
            }

            public Entry Entry { get; }
            public int? Index { get; set; }
        }

        //https://github.com/muukii/ListDiff/blob/master/Sources/ListDiff.swift
        public IDiffResultType Diffing<T, K>(List<T> oldList, List<T> newList, IEqualityComparer<T> isEqual)
            where T : IDiffable<K>
        {
            var table = new Dictionary<K, Entry>();

            //Pass 1
            var newRecords = new List<Record>();
            foreach (var newRecord in newList)
            {
                var key = newRecord.DiffIdentifier;
                if (table.TryGetValue(key, out var entry))
                {
                    entry.PushNew(null);
                    newRecords.Add(new Record(entry));
                }
                else
                {
                    var anotherEntry = new Entry();
                    anotherEntry.PushNew(null);
                    table[key] = anotherEntry;
                    newRecords.Add(new Record(anotherEntry));
                }
            }

            //Pass 2
            var oldRecords = new List<Record>();
            var reversed = new List<T>(oldList);
            reversed.Reverse();
            for (var i = 0; i < reversed.Count; i++)
            {
                var oldRecord = reversed[i];
                var key = oldRecord.DiffIdentifier;
                if (table.TryGetValue(key, out var entry))
                {
                    entry.PushOld(reversed.Count - 1 - i);
                    oldRecords.Add(new Record(entry));
                }
                else
                {
                    var anotherEntry = new Entry();
                    anotherEntry.PushOld(reversed.Count - 1 - i);
                    table[key] = anotherEntry;
                    oldRecords.Add(new Record(anotherEntry));
                }
            }

            //Pass 3
            var enumerated = GetRecordsEnumerated(newRecords);
            var filteredRecords = enumerated.Where(f => f.Record.Entry.OccurOnBothSides).ToList();
            foreach (var newRecord in filteredRecords)
            {
                var entry = newRecord.Record.Entry;
                var oldIndex = entry.OldIndexes.Pop();
                if (oldIndex == null)
                {
                    continue;
                }

                if (oldIndex.Value < oldList.Count)
                {
                    var n = newList[newRecord.Index];
                    var o = oldList[oldIndex.Value];
                    if (!isEqual.Equals(n, o))
                    {
                        entry.Updated = true;
                    }
                }

                newRecords[newRecord.Index].Index = oldIndex.Value;
                oldRecords[oldIndex.Value].Index = newRecord.Index;
            }

            var result = new DiffResult<K>();

            var runningOffset = 0;
            var deleteOffsets = new List<int>();
            for (var kk = 0; kk < oldRecords.Count; kk++)
            {
                var deleteOffset = runningOffset;
                if (oldRecords[kk].Index == null)
                {
                    result.Deletes.Add(kk);
                    runningOffset++;
                }

                result.OldMap[oldList[kk].DiffIdentifier] = kk;
                deleteOffsets.Add(deleteOffset);
            }

            runningOffset = 0;

            var insertOffsets = new List<int>();
            for (var nn = 0; nn < newRecords.Count; nn++)
            {
                var insertOffset = runningOffset;
                var insertOldIndex = newRecords[nn].Index;
                if (insertOldIndex != null)
                {
                    if (newRecords[nn].Entry.Updated)
                    {
                        result.Updates.Add(insertOldIndex.Value);
                    }

                    var deleteOffset = deleteOffsets[insertOldIndex.Value];
                    if (insertOldIndex.Value - deleteOffset + insertOffset != nn)
                    {
                        result.Moves.Add(new MoveIndex(insertOldIndex.Value, nn));
                    }
                }
                else
                {
                    result.Inserts.Add(nn);
                    runningOffset++;
                }

                result.NewMap[newList[nn].DiffIdentifier] = nn;
                insertOffsets.Add(insertOffset);
            }

            Debug.WriteLineIf(!result.Validate<T, K>(oldList, newList),
                $"Sanity check failed applying {result.Inserts.Count} inserts and {result.Deletes.Count} deletes to old count {oldList.Count} equaling new count {newList.Count}");

            return result;
        }

        private List<(int Index, Record Record)> GetRecordsEnumerated(List<Record> records)
        {
            var res = new List<(int Index, Record Record)>();
            for (var i = 0; i < records.Count; i++)
            {
                res.Add((i, records[i]));
            }

            return res;
        }
    }
}