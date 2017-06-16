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
		public event Action<IEntity> OnAdded;
		public event Action<IEntity> OnRemoved;
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
			OnAdded?.Invoke(item);
		}

		public void Clear()
		{
			foreach(var entity in _entities) {
				OnRemoved?.Invoke(entity);
			}
			_entities.Clear();
		}

		public bool Contains(IEntity item)
		{
			return _entities.Contains(item);
		}

		public bool Remove(IEntity item)
		{
			var hadEntity = _entities.Remove(item);
			if (hadEntity)
				OnRemoved?.Invoke(item);
			return hadEntity;
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