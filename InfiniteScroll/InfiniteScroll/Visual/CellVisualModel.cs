using InfiniteScroll.Enumerables;
using InfiniteScroll.InfinityScroll.Diff;

namespace InfiniteScroll.Visual
{
    public struct CellVisualModel : IDiffable<string>
    {
        public string DiffIdentifier => Data.ToString() + StorageType.ToString();
        
        public int Data { get; set; }
        public EStorageType StorageType { get; }

        public CellVisualModel(int data, EStorageType storageType)
        {
            Data = data;
            StorageType = storageType;
        }
    }
}