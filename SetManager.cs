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
		private readonly IEnumerable<Entity> _entities;
		private readonly Dictionary<Predicate<Entity>, EntitySet> _entitySets;

		public SetManager(IEnumerable<Entity> entities)
		{
			_entities = entities;
			_entitySets = new Dictionary<Predicate<Entity>, EntitySet>();
		}

		/// <summary>
		/// Returns all entitySet that have the specified components.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		public EntitySet SetContaining(Predicate<Entity> predicate)
		{
			if (_entitySets.ContainsKey(predicate))
				return _entitySets[predicate];
			var entities = CreateEntitySet(predicate);
			return entities;
		}

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		public void UpdateEntityMembership(Entity entity)
		{
			foreach (var kvp in _entitySets) {
				var set = kvp.Value;
				if (set.Matches(entity))
					set.Add(entity);
				else
					set.Remove(entity);
			}
		}

		/// <summary>
		/// Checks if the entity should be in the types list.
		/// </summary>
		/// <returns>true if the entity matches</returns>
		public static bool EntityMatchesTypes(Entity entity, params Type[] types)
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

		/// <summary>
		/// Make a new entity set.
		/// </summary>
		/// <returns></returns>
		private EntitySet CreateEntitySet(Predicate<Entity> predicate)
		{
			var entitySet = new EntitySet(predicate);
			_entitySets[predicate] = entitySet;
			foreach (var entity in _entities) {
				if (entitySet.Matches(entity))
					entitySet.Add(entity);
			}
			return entitySet;
		}
	}
}