// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
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
		private readonly Dictionary<EntitySet.IncludeInSet, EntitySet> _entitySets;

		public SetManager(IEnumerable<IEntity> entities)
		{
			_entities = entities;
			_entitySets = new Dictionary<EntitySet.IncludeInSet, EntitySet>();
		}

		/// <summary>
		/// Makes a new set and registers it for updating membership later.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			var entitySet = new EntitySet(predicate);
			_entitySets[predicate] = entitySet;
			foreach (var entity in _entities) {
				if (!entitySet.Matches(entity))
					continue;
				foreach (var component in entity)
					entitySet.Add(entity, null, component);
			}
			return entitySet;
		}

		/// <summary>
		/// Unregisters a set so it will no longer get membership updates.
		/// </summary>
		public void RemoveSet(EntitySet set)
		{
			var keys = new List<EntitySet.IncludeInSet>();
			foreach (var kvp in _entitySets) {
				if (kvp.Value == set)
					keys.Add(kvp.Key);
			}
			foreach (var key in keys) {
				_entitySets.Remove(key);
			}
		}

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		public void ComponentAdded(IEntity entity, object component)
		{
			UpdateSets(entity, null, component);
		}

		public void ComponentReplaced(IEntity entity, object oldComponent, object component)
		{
			UpdateSets(entity, oldComponent, component);
		}

		public void ComponentRemoved(IEntity entity, object oldComponent)
		{
			UpdateSets(entity, oldComponent, null);
		}

		private void UpdateSets(IEntity entity, object old, object component)
		{
			foreach (var kvp in _entitySets) {
				var set = kvp.Value;
				if (set.Matches(entity))
					set.Add(entity, old, component);
				else
					set.Remove(entity, old, component);
			}
		}


		/// <summary>
		/// Checks if the entity should be in the types list.
		/// </summary>
		/// <returns>true if the entity matches</returns>
		public static bool EntityMatchesTypes(IEntity entity, params Type[] types)
		{
			var all = false;
			foreach (var type in types) {
				if (entity.Contains(type)) {
					all = true;
				} else {
					all = false;
					break;
				}
			}
			return all;
		}
	}
}