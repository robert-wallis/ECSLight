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
	public class Context : IEnumerable<IEntity>, IDisposable
	{
		public ISetManager SetManager { get; }
		private readonly IEntityManager _entityManager;

		public Context()
		{
			var entities = new List<IEntity>();
			SetManager = new SetManager(entities);
			var componentManager = new ComponentManager(SetManager);
			_entityManager = new EntityManager(entities, componentManager);
		}

		public void Dispose()
		{
			_entityManager.ReleaseAll();
		}

		/// <summary>
		/// Dependency Constructor.  For full control of context.
		/// </summary>
		public Context(IEntityManager entityManager, ISetManager setManager)
		{
			_entityManager = entityManager;
			SetManager = setManager;
		}

		/// <summary>
		/// Make a new entity, or recycle an unused entity.
		/// </summary>
		/// <returns>new empty entity</returns>
		public IEntity CreateEntity(string name = "")
		{
			return _entityManager.CreateEntity(name);
		}

		/// <summary>
		/// End the lifecycle of the entity.
		/// Release the entity back to be reused later.
		/// </summary>
		/// <param name="entity">IEntity to be released.</param>
		public void ReleaseEntity(IEntity entity)
		{
			_entityManager.ReleaseEntity(entity);
		}

		/// <summary>
		/// Returns all entities that match the predicate.
		/// </summary>
		/// <returns>An enumerable list of entities, that will update automatically.</returns>
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			return SetManager.CreateSet(predicate);
		}

		/// <summary>
		/// Enumerator for all the entities.
		/// </summary>
		public IEnumerator<IEntity> GetEnumerator()
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