// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntitySet : IEnumerable<IEntity>
	{
		public delegate bool IncludeInSet(IEntity entity);

		public delegate void ComponentChanged(IEntity entity, object oldComponent, object newComponent);

		public IncludeInSet Predicate { get; }
		public int Count => _entities.Count;
		public event ComponentChanged OnAdded;
		public event ComponentChanged OnRemoved;

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

		public void Add(IEntity item, object old = null, object component = null)
		{
			_entities.Add(item);
			OnAdded?.Invoke(item, old, component);
		}

		public bool Remove(IEntity item, object old, object component = null)
		{
			var hadEntity = _entities.Remove(item);
			if (hadEntity)
				OnRemoved?.Invoke(item, old, component);
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