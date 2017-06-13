// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// Bag is like a List but adds and removes more quickly by not keeping order.
	/// </summary>
	class Bag<T> : ICollection<T>
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

		public bool Remove(T item)
		{
			// TODO: swap with end, and delete end
			return _list.Remove(item);
		}
	}
}