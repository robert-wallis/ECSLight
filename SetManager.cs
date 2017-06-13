// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public class SetManager : ISetManager
	{
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly Dictionary<HashSet<Type>, EntitySet> _matchers;

		public SetManager(Dictionary<Entity, Dictionary<Type, IComponent>> entities)
		{
			_entities = entities;
			_matchers = new Dictionary<HashSet<Type>, EntitySet>();
		}

		/// <summary>
		/// Returns all entities that have the specified components.
		/// </summary>
		/// <returns>An enumerable list of entities, that will update automatically.</returns>
		public EntitySet SetContaining(params Type[] types)
		{
			var matchTypes = new HashSet<Type>(types);
			foreach (var kvp in _matchers) {
				var matcher = kvp.Key;
				if (matcher.SetEquals(matchTypes)) {
					return kvp.Value;
				}
			}
			var entities = CreateEntitySet(matchTypes);
			return entities;
		}

		/// <summary>
		/// Make a new entity set.
		/// </summary>
		/// <param name="matchTypes"></param>
		/// <returns></returns>
		private EntitySet CreateEntitySet(HashSet<Type> matchTypes)
		{
			var matchSet = new EntitySet();
			// include entities that have the same type
			foreach (var kvp in _entities) {
				var entity = kvp.Key;
				var components = kvp.Value;
				foreach (var type in matchTypes) {
					if (!components.ContainsKey(type))
						continue;
					matchSet.Add(entity);
					break; // no need to add again if another type matches
				}
			}
			_matchers[matchTypes] = matchSet;
			return matchSet;
		}

		/// <summary>
		/// add entity to all matching sets 
		/// </summary>
		public void AddEntityToSets(Entity entity, Type type)
		{
			foreach (var kvp in _matchers) {
				var types = kvp.Key;
				var entities = kvp.Value;
				if (types.Contains(type)) {
					entities.Add(entity);
				}
			}
		}

		/// <summary>
		/// remove entity from any matchers that it no longer qualifies for
		/// </summary>
		public void RemoveEntityFromSets(Entity entity, Type type)
		{
			foreach (var kvp in _matchers) {
				var types = kvp.Key;
				var entities = kvp.Value;
				if (types.Contains(type)) {
					entities.Remove(entity);
				}
			}
		}
	}
}