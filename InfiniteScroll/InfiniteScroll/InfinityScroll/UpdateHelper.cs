using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using InfiniteScroll.Collection;
using InfiniteScroll.InfinityScroll.Diff;
using UIKit;

namespace InfiniteScroll.InfinityScroll
{
    public delegate void UpdateCompletion(bool isInsertionsPresent = false);
    public class UpdateHelper<T> where T : /*class,*/ IDiffable<string>
    {
        public volatile bool IsUpdateInProgress;
        public event UpdateCompletion UpdateCompleted;

        private class EqualityComparer : IEqualityComparer<T>
        {
            public bool Equals(T x, T y)
            {
                return x?.Equals(y) ?? false;
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }
        }

        public void UpdateList(InfiniteCollectionView collectionView,
            List<T> newItems,
            List<T> prevItems)
        {
            collectionView.InvokeOnMainThread(() =>
            {
                var diffs = GetDiffsNew(newItems, prevItems);
                if (diffs.HasChanges)
                {
                    IsUpdateInProgress = true;
                    ApplyChatChangesOnUi(diffs, collectionView, newItems.Count);
                }
            });
        }

        private IDiffResultType GetDiffsNew(List<T> newItems,
            List<T> prevItems)
        {
            var diff = new Diff.Diff();
            var diffs = diff.Diffing<T, string>(prevItems, newItems, new EqualityComparer());
            return diffs;
        }

        private void ApplyChatChangesOnUi(IDiffResultType diffs,
            InfiniteCollectionView collectionView, int cellsCount)
        {
            var insertedItems = GetIndexPaths(diffs.Inserts);
            var deletedItems = GetIndexPaths(diffs.Deletes);
            var modifiedItems = GetIndexPaths(diffs.Updates);
            var movedItems = GetIndexPathsForMoves(diffs.Moves);
            //Insertion to bottom
            var maxIndexPath = NSIndexPath.FromItemSection(cellsCount - 1, 0);
            var firstIndexPath = NSIndexPath.FromItemSection(1, 0);
            if (insertedItems.Length == 0 && deletedItems.Length == 0
                                          && modifiedItems.Length == 0 && movedItems.Length == 0)
            {
                IsUpdateInProgress = false;
                return;
            }

            if (insertedItems.Length > 0
                && insertedItems.ToList().Contains(maxIndexPath))
            {
                collectionView.PerformBatchUpdates(
                    () =>
                    {
                        ApplyDiffsOnCollectionView(collectionView, insertedItems, deletedItems, modifiedItems,
                            movedItems);
                    },
                    completion =>
                    {
                        //collectionView.ScrollCollectionViewToBottomIfPossible(true);
                        IsUpdateInProgress = false;
                        UpdateCompleted?.Invoke(true);
                    });
            }
            //updates
            else if (modifiedItems.Length != 0 && insertedItems.Length == 0 && deletedItems.Length == 0)
            {
                collectionView.PerformBatchUpdates(
                    () =>
                    {
                        ApplyDiffsOnCollectionView(collectionView, insertedItems, deletedItems, modifiedItems,
                            movedItems);
                    }, completion =>
                    {
                        IsUpdateInProgress = false;
                        UpdateCompleted?.Invoke();
                    });
            }
            //delete
            else if (deletedItems.Length != 0 && insertedItems.Length == 0 && modifiedItems.Length == 0)
            {
                collectionView.PerformBatchUpdates(
                    () =>
                    {
                        ApplyDiffsOnCollectionView(collectionView, insertedItems, deletedItems, modifiedItems,
                            movedItems);
                    }, completion1 =>
                    {
                        IsUpdateInProgress = false;
                        UpdateCompleted?.Invoke();
                    });
            }
            //insertion to top
            else if (insertedItems.Length > 0
                     && insertedItems.ToList().Contains(firstIndexPath))
            {
                collectionView.IsInsertingCellsToTop = true;
                UIView.PerformWithoutAnimation(() =>
                {
                    collectionView.PerformBatchUpdates(
                        () =>
                        {
                            ApplyDiffsOnCollectionView(collectionView, insertedItems, deletedItems, modifiedItems,
                                movedItems);
                        }, completion =>
                        {
                            collectionView.IsInsertingCellsToTop = false;
                            IsUpdateInProgress = false;
                            UpdateCompleted?.Invoke(true);
                        });
                });
            }
            else
            {
                UIView.PerformWithoutAnimation(() =>
                {
                    collectionView.PerformBatchUpdates(
                        () =>
                        {
                            ApplyDiffsOnCollectionView(collectionView, insertedItems, deletedItems, modifiedItems,
                                movedItems);
                        }, completion =>
                        {
                            IsUpdateInProgress = false;
                            UpdateCompleted?.Invoke();
                        });
                });
            }
        }

        private static void ApplyDiffsOnCollectionView(
            InfiniteCollectionView collectionView,
            NSIndexPath[] insertedItems,
            NSIndexPath[] deletedItems,
            NSIndexPath[] modifiedItems,
            (NSIndexPath From, NSIndexPath To)[] movedItems)
        {
            collectionView.DeleteItems(deletedItems);
            collectionView.InsertItems(insertedItems);
            collectionView.ReloadItems(modifiedItems);
            foreach (var (@from, to) in movedItems)
            {
                collectionView.MoveItem(@from, to);
            }
        }

        private static NSIndexPath[] GetIndexPaths(HashSet<int> diffs)
        {
            var res = new List<NSIndexPath>();
            foreach (var item in diffs)
            {
                res.Add(NSIndexPath.FromItemSection(item, 0));
            }

            return res.ToArray();
        }

        private static (NSIndexPath From, NSIndexPath To)[] GetIndexPathsForMoves(List<MoveIndex> moves)
        {
            var res = new List<(NSIndexPath From, NSIndexPath To)>();
            foreach (var item in moves)
            {
                var tuple = (NSIndexPath.FromItemSection(item.From, 0), NSIndexPath.FromItemSection(item.To, 0));
                res.Add(tuple);
            }

            return res.ToArray();
        }
    }
}
