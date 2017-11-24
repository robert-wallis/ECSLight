// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// Keeps track of all the entity sets.
	/// Adds and removes entities to the appropriate sets.
	/// </summary>
	public class SetManager : ISetManager
	{
		private readonly IEnumerable<IEntity> _entities;
		private readonly List<EntitySet> _entitySets;

		public SetManager(IEnumerable<IEntity> entities)
		{
			_entities = entities;
			_entitySets = new List<EntitySet>();
		}

		/// <summary>
		/// Makes a new set and registers it for updating membership later.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			var entitySet = new EntitySet(predicate);
			_entitySets.Add(entitySet);
			foreach (var entity in _entities) {
				if (!entitySet.Matches(entity))
					continue;
				entitySet.Add(entity);
			}
			return entitySet;
		}

		/// <summary>
		/// Unregisters a set so it will no longer get membership updates.
		/// </summary>
		public void RemoveSet(EntitySet set)
		{
			_entitySets.Remove(set);
		}

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		public void UpdateSets(IEntity entity)
		{
			foreach (var set in _entitySets) {
				if (set.Matches(entity))
					set.Add(entity);
				else
					set.Remove(entity);
			}
		}
	}
}