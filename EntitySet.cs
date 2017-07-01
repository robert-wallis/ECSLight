// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntitySet : IEnumerable<IEntity>
	{
		public delegate bool IncludeInSet(IEntity entity);

		public delegate void ComponentAddRemove(IEntity entity, object component);

		public delegate void ComponentChanged(IEntity entity, object oldComponent, object newComponent);

		public IncludeInSet Predicate { get; }
		public int Count => _entities.Count;
		public event ComponentAddRemove OnAdded;
		public event ComponentChanged OnReplaced;
		public event ComponentAddRemove OnRemoved;

		private readonly HashSet<IEntity> _entities = new HashSet<IEntity>();

		public EntitySet(IncludeInSet predicate)
		{
			Predicate = predicate;
		}

		public bool Matches(IEntity entity)
		{
			return Predicate.Invoke(entity);
		}

		public bool Contains(IEntity item)
		{
			return _entities.Contains(item);
		}

		public void Add(IEntity item, object component = null)
		{
			_entities.Add(item);
			OnAdded?.Invoke(item, component);
		}

		public void Replace(IEntity item, object oldComponent, object newComponent)
		{
			if (_entities.Contains(item)) {
				OnReplaced?.Invoke(item, oldComponent, newComponent);
			} else {
				Add(item, newComponent);
			}
		}

		public bool Remove(IEntity item, object old = null)
		{
			var hadEntity = _entities.Remove(item);
			if (hadEntity)
				OnRemoved?.Invoke(item, old);
			return hadEntity;
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}