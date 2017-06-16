// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// A context manages a set of entities and matchers.
	/// For example, a game could have a 'board' context with 'piece' entities.
	/// And a multiplayer game could have multiple 'board' contexts.
	/// </summary>
	public class Context : IEnumerable<Entity>
	{
		private readonly IEntityManager _entityManager;
		private readonly ISetManager _setManager;

		public Context()
		{
			var entities = new Dictionary<Entity, Dictionary<Type, IComponent>>();
			_setManager = new SetManager(entities.Keys);
			var componentManager = new ComponentManager(entities, _setManager);
			_entityManager = new EntityManager(entities, componentManager);
		}

		/// <summary>
		/// Dependency Constructor.  For full control of context.
		/// </summary>
		/// <param name="entityManager">entity manager</param>
		/// <param name="setManager">entity set manager</param>
		public Context(IEntityManager entityManager, ISetManager setManager)
		{
			_entityManager = entityManager;
			_setManager = setManager;
		}

		/// <summary>
		/// Make a new entity, or recycle an unused entity.
		/// </summary>
		/// <returns>new empty entity</returns>
		public Entity CreateEntity(string name = "")
		{
			return _entityManager.CreateEntity(name);
		}

		/// <summary>
		/// End the lifecycle of the entity.
		/// Release the entity back to be reused later.
		/// </summary>
		/// <param name="entity">Entity to be released.</param>
		public void ReleaseEntity(Entity entity)
		{
			_entityManager.ReleaseEntity(entity);
		}

		/// <summary>
		/// Returns all entities that match the predicate.
		/// </summary>
		/// <returns>An enumerable list of entities, that will update automatically.</returns>
		public EntitySet SetContaining(Predicate<Entity> predicate)
		{
			return _setManager.SetContaining(predicate);
		}

		/// <summary>
		/// Enumerator for all the entities.
		/// </summary>
		public IEnumerator<Entity> GetEnumerator()
		{
			return _entityManager.GetEnumerator();
		}

		/// <summary>
		/// Enumerator for all the entities.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}