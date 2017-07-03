// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECSLight
{
	public class EntitySet : IEnumerable<IEntity>
	{
		public delegate bool IncludeInSet(IEntity entity);

		public IncludeInSet Predicate { get; }
		public int Count => _entities.Count;

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

		public void Add(IEntity item)
		{
			_entities.Add(item);
		}

		public bool Remove(IEntity item)
		{
			return _entities.Remove(item);
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return _entities.ToList().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}