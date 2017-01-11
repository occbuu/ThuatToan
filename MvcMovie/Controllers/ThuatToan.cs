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
    }

    public class ThuatToan
    {
        void Swap(SinhVien[] items, int left, int right)
        {
            if (left != right) { SinhVien temp = items[left]; items[left] = items[right]; items[right] = temp; }
        }

        public void Sort(SinhVien[] items, string type)
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
                    }
                    
                }
            } while (swapped != false);
        }
    }


}