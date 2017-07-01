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
		private readonly HashSet<EntitySet> _enabledSets;
		private readonly HashSet<EntitySet> _disabledSets;

		public SetManager(IEnumerable<IEntity> entities)
		{
			_entities = entities;
			_enabledSets = new HashSet<EntitySet>();
			_disabledSets = new HashSet<EntitySet>();
		}

		/// <summary>
		/// Makes a new set and registers it for updating membership later.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			var entitySet = new EntitySet(predicate);
			_enabledSets.Add(entitySet);
			MatchAllEntitiesToNewSet(entitySet);
			return entitySet;
		}

		/// <summary>
		/// Unregisters a set so it will no longer get membership updates.
		/// </summary>
		public void RemoveSet(EntitySet set)
		{
			_enabledSets.Remove(set);
		}

		/// <summary>
		/// Add or Remove components to this set when Components change.
		/// Undo a DisableSet.
		/// Default is enabled.
		/// </summary>
		public void EnableSet(EntitySet set)
		{
			_disabledSets.Remove(set);
			_enabledSets.Add(set);
			MatchAllEntitiesToEnabledSet(set);
		}

		/// <summary>
		/// Don't Add or Remove components to this set when Components change.
		/// Default state is enabled.
		/// </summary>
		/// <param name="set"></param>
		public void DisableSet(EntitySet set)
		{
			_enabledSets.Remove(set);
			_disabledSets.Add(set);
		}

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		public void ComponentAdded(IEntity entity, object component)
		{
			foreach (var set in _enabledSets) {
				if (set.Matches(entity)) {
					set.Add(entity);
					set.ComponentAdded(entity, component);
				} else {
					set.ComponentRemoved(entity, null);
					set.Remove(entity);
				}
			}
		}

		/// <summary>
		/// Replace entity on matching sets, remove from unmatching sets.
		/// </summary>
		public void ComponentReplaced(IEntity entity, object oldComponent, object newComponent)
		{
			foreach (var set in _enabledSets) {
				if (set.Matches(entity)) {
					set.Add(entity);
					set.ComponentReplaced(entity, oldComponent, newComponent);
				} else {
					set.ComponentRemoved(entity, oldComponent);
					set.Remove(entity);
				}
			}
		}

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		public void ComponentRemoved(IEntity entity, object oldComponent)
		{
			foreach (var set in _enabledSets) {
				if (set.Matches(entity)) {
					set.Add(entity);
					set.ComponentAdded(entity, null);
				} else {
					set.ComponentRemoved(entity, oldComponent);
					set.Remove(entity);
				}
			}
		}

		/// <summary>
		/// Check all the entities for membership and Add to the set.
		/// </summary>
		private void MatchAllEntitiesToNewSet(EntitySet set)
		{
			foreach (var entity in _entities) {
				if (!set.Matches(entity))
					continue;
				set.Add(entity);
				set.ComponentAdded(entity, null);
			}
		}

		/// <summary>
		/// Check all the entities for membership and Add or Remove from the set.
		/// </summary>
		private void MatchAllEntitiesToEnabledSet(EntitySet set)
		{
			foreach (var entity in _entities) {
				if (set.Matches(entity)) {
					set.ComponentReplaced(entity, null, null);
					set.Add(entity);
				} else {
					set.ComponentRemoved(entity, null);
					set.Remove(entity);
				}
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