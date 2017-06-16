// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntitySet : ICollection<IEntity>
	{
		public Predicate<IEntity> Predicate { get; }
		public int Count => _entities.Count;
		bool ICollection<IEntity>.IsReadOnly => false;

		private readonly HashSet<IEntity> _entities = new HashSet<IEntity>();

		public EntitySet(Predicate<IEntity> predicate)
		{
			Predicate = predicate;
		}

		public bool Matches(IEntity entity)
		{
			return Predicate.Invoke(entity);
		}

		public void Add(IEntity item)
		{
			_entities.Add(item);
		}

		public void Clear()
		{
			_entities.Clear();
		}

		public bool Contains(IEntity item)
		{
			return _entities.Contains(item);
		}

		public bool Remove(IEntity item)
		{
			return _entities.Remove(item);
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		void ICollection<IEntity>.CopyTo(IEntity[] array, int arrayIndex)
		{
			_entities.CopyTo(array, arrayIndex);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}