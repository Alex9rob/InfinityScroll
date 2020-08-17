using System;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.Entities
{
    public class Number : IComparable
    {
        public int NumberData { get; }
        public EStorageType StorageType { get; }

        public Number(int number, EStorageType storageType)
        {
            NumberData = number;
            StorageType = storageType;
        }

        public int CompareTo(object obj)
        {

            if(obj is Number other)
            {
                if (other.NumberData > NumberData)
                {
                    return 1; 
                }
            }

            return 0;
        }
    }
}