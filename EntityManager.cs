// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntityManager : IEntityManager
	{
		private readonly ICollection<IEntity> _entities;
		private readonly IComponentManager _componentManager;

		public EntityManager(ICollection<IEntity> entities, IComponentManager componentManager)
		{
			_entities = entities;
			_componentManager = componentManager;
		}

		/// <summary>
		/// Make a new entity, or recycle an unused entity.
		/// </summary>
		/// <returns>new empty entity</returns>
		public IEntity CreateEntity(string name = "")
		{
			var entity = new Entity(this, _componentManager, name);
			_entities.Add(entity);
			return entity;
		}

		/// <summary>
		/// Release the entity back to be reused later.
		/// </summary>
		/// <param name="entity">IEntity to be released.</param>
		public void ReleaseEntity(IEntity entity)
		{
			var types = new List<Type>();
			foreach (var component in entity) {
				types.Add(component.GetType());
			}
			foreach (var type in types) {
				_componentManager.RemoveComponent(entity, type);
			}
			_entities.Remove(entity);
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