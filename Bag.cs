// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// Bag is like a List but adds and removes more quickly by not maintaining order.
	/// </summary>
	public class Bag<T> : ICollection<T>
	{
		public int Count => _list.Count;
		public bool IsReadOnly => false;

		private readonly List<T> _list;

		public Bag(int capacity = 0)
		{
			_list = new List<T>(capacity);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			_list.Add(item);
		}

		public void Clear()
		{
			_list.Clear();
		}

		public bool Contains(T item)
		{
			return _list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes item and replaces it with the end of the list.  Then removes the end.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>true if an item was removed</returns>
		public bool Remove(T item)
		{
			var index = _list.IndexOf(item);
			if (index == -1)
				return false;
			var last = _list.Count - 1;
			_list[index] = _list[last];
			_list.RemoveAt(last);
			return true;
		}
	}
}