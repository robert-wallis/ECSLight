// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntitySet : ICollection<Entity>
	{
		public Predicate<Entity> Predicate { get; }
		public int Count => _entities.Count;
		bool ICollection<Entity>.IsReadOnly => false;

		private readonly HashSet<Entity> _entities = new HashSet<Entity>();

		public EntitySet(Predicate<Entity> predicate)
		{
			Predicate = predicate;
		}

		public bool Matches(Entity entity)
		{
			return Predicate.Invoke(entity);
		}

		public void Add(Entity item)
		{
			_entities.Add(item);
		}

		public void Clear()
		{
			_entities.Clear();
		}

		public bool Contains(Entity item)
		{
			return _entities.Contains(item);
		}

		public bool Remove(Entity item)
		{
			return _entities.Remove(item);
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		void ICollection<Entity>.CopyTo(Entity[] array, int arrayIndex)
		{
			_entities.CopyTo(array, arrayIndex);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}