using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Controllers
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public Set() { }

        public Set(IEnumerable<T> items) { AddRange(items); }

        public void Add(T item)
        {
            if (Contains(item)) { throw new InvalidOperationException("Item already exists in Set"); }
  
            _items.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public bool Remove(T item) { return _items.Remove(item); }
        public bool Contains(T item) { return _items.Contains(item); }

        public int Count { get { return _items.Count; } }

        public IEnumerator<T> GetEnumerator() { return _items.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return _items.GetEnumerator(); }
        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items) { if (!Contains(item)) { result.Add(item); } }

            return result;
        }

        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();

            foreach (T item in _items) { if (other._items.Contains(item)) { result.Add(item); } }

            return result;
        }

        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
            {
                result.Remove(item);
            }

            return result;
        }


        public Set<T> SymmetricDifference(Set<T> other)
        {
            Set<T> union = Union(other); Set<T> intersection = Intersection(other);

            return union.Difference(intersection);
        }

        void Swap(T[] items, int left, int right)
        {
            if (left != right) { T temp = items[left]; items[left] = items[right]; items[right] = temp; }
        }

    }

    public class SinhVien
    {
        public int STT { get; set; }
        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }

        public int CompareTo_STT (SinhVien temp)
        {
            if (this.STT > temp.STT)
                return 1;
            else
                return 0;
        }

        public int CompareTo_MSSV(SinhVien temp)
        {
            if (this.MSSV.CompareTo(temp.MSSV) > 0)
                return 1;
            else
                return 0;
        }
        public int CompareTo_HoTen(SinhVien temp)
        {
            if (this.HoTen.CompareTo(temp.HoTen) > 0)
                return 1;
            else
                return 0;
        }
        public int CompareTo_NgaySinh(SinhVien temp)
        {
            if (this.NgaySinh > temp.NgaySinh)
                return 1;
            else
                return 0;
        }
    }

    public class ThuatToan
    {
        void Swap(SinhVien[] items, int left, int right)
        {
            if (left != right) { SinhVien temp = items[left]; items[left] = items[right]; items[right] = temp; }
        }

        //Bubble Sort
        public void BubbleSort(SinhVien[] items, string type)
        {
            bool swapped;

            do
            {
                swapped = false; for (int i = 1; i < items.Length; i++)
                {
                    switch(type)
                        {
                            case "STT":
                            {
                                if (items[i - 1].CompareTo_STT(items[i]) > 0)
                                { Swap(items, i - 1, i); swapped = true; }
                                break;
                            }
                            case "MSSV":
                            {
                                if (items[i - 1].CompareTo_MSSV(items[i]) > 0)
                                { Swap(items, i - 1, i); swapped = true; }
                                break;
                            }
                        case "HoTen":
                            {
                                if (items[i - 1].CompareTo_HoTen(items[i]) > 0)
                                { Swap(items, i - 1, i); swapped = true; }
                                break;
                            }
                        case "NgaySinh":
                            {
                                if (items[i - 1].CompareTo_NgaySinh(items[i]) > 0)
                                { Swap(items, i - 1, i); swapped = true; }
                                break;
                            }
                    }
                    
                }
            } while (swapped != false);
        }

        //Insertion Sort
        private int FindInsertionIndex(SinhVien[] items, SinhVien valueToInsert,string type)
        {
            switch(type)
            {
                case "STT":
                    {
                        for (int index = 0; index < items.Length; index++)
                        {
                            if (items[index].CompareTo_STT(valueToInsert) > 0)
                            {
                                return index;
                            }
                        }
                        break;
                    }
                case "MSSV":
                    {
                        for (int index = 0; index < items.Length; index++)
                        {
                            if (items[index].CompareTo_MSSV(valueToInsert) > 0)
                            {
                                return index;
                            }
                        }
                        break;
                    }
                case "HoTen":
                    {
                        for (int index = 0; index < items.Length; index++)
                        {
                            if (items[index].CompareTo_HoTen(valueToInsert) > 0)
                            {
                                return index;
                            }
                        }
                        break;
                    }
                case "NgaySinh":
                    {
                        for (int index = 0; index < items.Length; index++)
                        {
                            if (items[index].CompareTo_NgaySinh(valueToInsert) > 0)
                            {
                                return index;
                            }
                        }
                        break;
                    }
            }
           



            throw new InvalidOperationException("The insertion index was not found");
        }        private void Insert(SinhVien[] itemArray, int indexInsertingAt, int indexInsertingFrom)
        {
            // itemArray = 0 1 2 4 5 6 3 7
            // insertingAt = 3
            // insertingFrom = 6
            // actions
            // 1: Store index at in temp temp = 4
            // 2: Set index at to index from -> 0 1 2 3 5 6 3 7 temp = 4
            // 3: Walking backward from index from to index at + 1.
            // Shift values from left to right once.
            // 0 1 2 3 5 6 6 7 temp = 4
            // 0 1 2 3 5 5 6 7 temp = 4
            // 4: Write temp value to index at + 1.
            // 0 1 2 3 4 5 6 7 temp = 4
            // Step 1.
            T temp = itemArray[indexInsertingAt];
            // Step 2.
            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];
            // Step 3.
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
               
             {
                itemArray[current] = itemArray[current - 1];
            }
            // Step 4.
            itemArray[indexInsertingAt + 1] = temp;
        }
        public void InsertionSort(SinhVien[] items,string type)
        {
            int sortedRangeEndIndex = 1;
            switch(type)
            {
                case "STT":
                    {
                        while (sortedRangeEndIndex < items.Length)
                        {
                            if (items[sortedRangeEndIndex].CompareTo_STT(items[sortedRangeEndIndex - 1]) < 0)
                            {
                                int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex], type);
                                Insert(items, insertIndex, sortedRangeEndIndex);
                            }
                            sortedRangeEndIndex++;
                        }
                        break;
                    }
                case "MSSV":
                    {
                        while (sortedRangeEndIndex < items.Length)
                        {
                            if (items[sortedRangeEndIndex].CompareTo_MSSV(items[sortedRangeEndIndex - 1]) < 0)
                            {
                                int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex], type);
                                Insert(items, insertIndex, sortedRangeEndIndex);
                            }
                            sortedRangeEndIndex++;
                        }
                        break;
                    }
                case "HoTen":
                    {
                        while (sortedRangeEndIndex < items.Length)
                        {
                            if (items[sortedRangeEndIndex].CompareTo_HoTen(items[sortedRangeEndIndex - 1]) < 0)
                            {
                                int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex], type);
                                Insert(items, insertIndex, sortedRangeEndIndex);
                            }
                            sortedRangeEndIndex++;
                        }
                        break;
                    }
                case "NgaySinh":
                    {
                        while (sortedRangeEndIndex < items.Length)
                        {
                            if (items[sortedRangeEndIndex].CompareTo_NgaySinh(items[sortedRangeEndIndex - 1]) < 0)
                            {
                                int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex], type);
                                Insert(items, insertIndex, sortedRangeEndIndex);
                            }
                            sortedRangeEndIndex++;
                        }
                        break;
                    }

            }
           
        }
        //Selection Sort

        private int FindIndexOfSmallestFromIndex(SinhVien[] items, int sortedRangeEnd, string type)
        {
            SinhVien currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            switch (type)
            {
                case "STT":
                    {
                        for (int i = sortedRangeEnd + 1; i < items.Length; i++)
                        {
                            if (currentSmallest.CompareTo_STT(items[i]) > 0)
                            {
                                currentSmallest = items[i];
                                currentSmallestIndex = i;
                            }
                        }
                        break;
                    }
                case "MSSV":
                    {
                        for (int i = sortedRangeEnd + 1; i < items.Length; i++)
                        {
                            if (currentSmallest.CompareTo_MSSV(items[i]) > 0)
                            {
                                currentSmallest = items[i];
                                currentSmallestIndex = i;
                            }
                        }
                        break;
                    }
                case "HoTen":
                    {
                        for (int i = sortedRangeEnd + 1; i < items.Length; i++)
                        {
                            if (currentSmallest.CompareTo_HoTen(items[i]) > 0)
                            {
                                currentSmallest = items[i];
                                currentSmallestIndex = i;
                            }
                        }
                        break;
                    }
                case "NgaySinh":
                    {
                        for (int i = sortedRangeEnd + 1; i < items.Length; i++)
                        {
                            if (currentSmallest.CompareTo_NgaySinh(items[i]) > 0)
                            {
                                currentSmallest = items[i];
                                currentSmallestIndex = i;
                            }
                        }
                        break;
                    }
            }

     
            return currentSmallestIndex;
        }

        public void SelectionSort(SinhVien[] items, string type)
        {
            int sortedRangeEnd = 0;
            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd,type);
                Swap(items, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
            }
        }

        //Merge Sort

        private void Merge(SinhVien[] items, SinhVien[] left, SinhVien[] right,string type)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;
            int remaining = left.Length + right.Length;
            switch (type)
            {
                case "STT":
                    {
                        while (remaining > 0)
                        {
                            if (leftIndex >= left.Length)
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            else if (rightIndex >= right.Length)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else if (left[leftIndex].CompareTo_STT(right[rightIndex]) < 0)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            targetIndex++;
                            remaining--;
                        }
                        break;
                    }
                case "MSSV":
                    {
                        while (remaining > 0)
                        {
                            if (leftIndex >= left.Length)
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            else if (rightIndex >= right.Length)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else if (left[leftIndex].CompareTo_MSSV(right[rightIndex]) < 0)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            targetIndex++;
                            remaining--;
                        }
                        break;
                    }
                case "HoTen":
                    {
                        while (remaining > 0)
                        {
                            if (leftIndex >= left.Length)
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            else if (rightIndex >= right.Length)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else if (left[leftIndex].CompareTo_HoTen(right[rightIndex]) < 0)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            targetIndex++;
                            remaining--;
                        }
                        break;
                    }
                case "NgaySinh":
                    {
                        while (remaining > 0)
                        {
                            if (leftIndex >= left.Length)
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            else if (rightIndex >= right.Length)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else if (left[leftIndex].CompareTo_NgaySinh(right[rightIndex]) < 0)
                            {
                                items[targetIndex] = left[leftIndex++];
                            }
                            else
                            {
                                items[targetIndex] = right[rightIndex++];
                            }
                            targetIndex++;
                            remaining--;
                        }
                        break;
                    }

            }
            
        }
        public void MergeSort(SinhVien[] items,string type)
        {
            if (items.Length <= 1)
            {
                return;
            }
            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;
            SinhVien[] left = new SinhVien[leftSize];
            SinhVien[] right = new SinhVien[rightSize];
            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);
            MergeSort(left, type);
            MergeSort(right, type);
            Merge(items, left, right, type);
        }
        //Quick Sort
        Random _pivotRng = new Random();
        private int partition(SinhVien[] items, int left, int right, int pivotIndex, string type)
        {
            SinhVien pivotValue = items[pivotIndex];
            Swap(items, pivotIndex, right);
            int storeIndex = left;
            switch (type)
            {
                case "STT":
                    {

                        for (int i = left; i < right; i++)
                        {

                            if (items[i].CompareTo_STT(pivotValue) < 0)
                            {
                                Swap(items, i, storeIndex);
                                storeIndex += 1;
                            }

                        }
                        break;
                    }
                case "MSSV":
                    {

                        for (int i = left; i < right; i++)
                        {

                            if (items[i].CompareTo_MSSV(pivotValue) < 0)
                            {
                                Swap(items, i, storeIndex);
                                storeIndex += 1;
                            }

                        }
                        break;
                    }
                case "HoTen":
                    {

                        for (int i = left; i < right; i++)
                        {

                            if (items[i].CompareTo_HoTen(pivotValue) < 0)
                            {
                                Swap(items, i, storeIndex);
                                storeIndex += 1;
                            }

                        }
                        break;
                    }
                case "NgaySinh":
                    {

                        for (int i = left; i < right; i++)
                        {

                            if (items[i].CompareTo_NgaySinh(pivotValue) < 0)
                            {
                                Swap(items, i, storeIndex);
                                storeIndex += 1;
                            }

                        }
                        break;
                    }
            }
            
            Swap(items, storeIndex, right);
            return storeIndex;
        }
        private void quick(SinhVien[] items, int left, int right,string type)
        {
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(items, left, right, pivotIndex,type);
                quick(items, left, newPivot - 1,type);
                quick(items, newPivot + 1, right,type);
            }
        }        public void QuickSort(SinhVien[] items,string type)
        {
            quick(items, 0, items.Length - 1,type);
        }


    }


}