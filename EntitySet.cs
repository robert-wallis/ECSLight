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

		public bool Contains(IEntity entity)
		{
			return _entities.Contains(entity);
		}

		public void Add(IEntity entity)
		{
			_entities.Add(entity);
		}

		public void Remove(IEntity entity)
		{
			_entities.Remove(entity);
		}

		public void ComponentAdded(IEntity entity, object component)
		{
			OnAdded?.Invoke(entity, component);
		}

		public void ComponentReplaced(IEntity entity, object oldComponent, object newComponent)
		{
			if (_entities.Contains(entity)) {
				OnReplaced?.Invoke(entity, oldComponent, newComponent);
			} else {
				ComponentAdded(entity, newComponent);
			}
		}

		public void ComponentRemoved(IEntity entity, object old)
		{
			OnRemoved?.Invoke(entity, old);
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