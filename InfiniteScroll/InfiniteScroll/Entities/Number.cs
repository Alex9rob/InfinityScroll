using InfiniteScroll.Enumerables;

namespace InfiniteScroll.Entities
{
    public class Number
    {
        public int NumberData { get; }
        public EStorageType StorageType { get; }

        public Number(int number, EStorageType storageType)
        {
            NumberData = number;
            StorageType = storageType;
        }
    }
}