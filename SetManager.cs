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
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly Dictionary<HashSet<Type>, HashSet<Entity>> _entitySets;

		public SetManager(Dictionary<Entity, Dictionary<Type, IComponent>> entities)
		{
			_entities = entities;
			_entitySets = new Dictionary<HashSet<Type>, HashSet<Entity>>();
		}

		/// <summary>
		/// Returns all entitySet that have the specified components.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		public HashSet<Entity> SetContaining(params Type[] types)
		{
			var matchTypes = new HashSet<Type>(types);
			foreach (var kvp in _entitySets) {
				var matcher = kvp.Key;
				if (matcher.SetEquals(matchTypes)) {
					return kvp.Value;
				}
			}
			var entities = CreateEntitySet(matchTypes);
			return entities;
		}

		/// <summary>
		/// add entity to all matching sets 
		/// </summary>
		public void AddEntityToAllSets(Entity entity)
		{
			foreach (var kvp in _entitySets) {
				var types = kvp.Key;
				var entities = kvp.Value;
				if (EntityMatchesTypes(entity, types))
					entities.Add(entity);
				else
					entities.Remove(entity);
			}
		}

		/// <summary>
		/// remove entity from any matchers that it no longer qualifies for
		/// </summary>
		public void RemoveEntityFromSets(Entity entity, Type type)
		{
			foreach (var kvp in _entitySets) {
				var types = kvp.Key;
				var entities = kvp.Value;
				if (types.Contains(type)) {
					entities.Remove(entity);
				}
			}
		}

		/// <summary>
		/// Make a new entity set.
		/// </summary>
		/// <param name="matchTypes"></param>
		/// <returns></returns>
		private HashSet<Entity> CreateEntitySet(HashSet<Type> matchTypes)
		{
			var entitySet = new HashSet<Entity>();
			_entitySets[matchTypes] = entitySet;
			foreach (var kvp in _entities) {
				if (EntityMatchesTypes(kvp.Key, matchTypes))
					entitySet.Add(kvp.Key);
			}
			return entitySet;
		}

		/// <summary>
		/// Checks if the entity should be in the types list.
		/// </summary>
		/// <returns>true if the entity matches</returns>
		private bool EntityMatchesTypes(Entity entity, HashSet<Type> types)
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