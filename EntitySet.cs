// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntitySet : IEnumerable<Entity>
	{
		public int Count => _entities.Count;

		private readonly Bag<Entity> _entities;

		public EntitySet()
		{
			_entities = new Bag<Entity>();
		}

		public void Add(Entity item)
		{
			_entities.Add(item);
		}

		public bool Remove(Entity item)
		{
			return _entities.Remove(item);
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}