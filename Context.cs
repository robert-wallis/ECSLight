// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// A context manages a set of entities and matchers.
	/// For example, a game could have a 'board' context with 'piece' entities.
	/// And a multiplayer game could have multiple 'board' contexts.
	/// </summary>
	public class Context
	{
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly Dictionary<HashSet<Type>, EntitySet> _matchers;

		public Context(int capacity = 128)
		{
			_entities = new Dictionary<Entity, Dictionary<Type, IComponent>>(capacity);
			_matchers = new Dictionary<HashSet<Type>, EntitySet>();
		}

		/// <summary>
		/// Make a new entity, or recycle an unused entity.
		/// </summary>
		/// <returns>new empty entity</returns>
		public Entity CreateEntity()
		{
			var entity = new Entity(this);
			return entity;
		}

		/// <summary>
		/// Release the entity back to be reused later.
		/// </summary>
		/// <param name="entity">Entity to be released.</param>
		public void ReleaseEntity(Entity entity)
		{
			// TODO: remove components from entity
		}

		/// <summary>
		/// Attach a component, or replace a component, with the new component.
		/// </summary>
		/// <typeparam name="TComponent">Type of component to attach.</typeparam>
		/// <param name="entity">Entity to which the component should be attached.</param>
		/// <param name="component">Component to attach to the entity.</param>
		public void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : class, IComponent
		{
			var type = typeof(TComponent);

			// add component to entity
			if (!_entities.ContainsKey(entity))
				_entities[entity] = new Dictionary<Type, IComponent>(1);
			_entities[entity][type] = component;

			// add entity to all matchers
			foreach (var kvp in _matchers) {
				var types = kvp.Key;
				var entities = kvp.Value;
				if (types.Contains(type)) {
					entities.Add(entity);
				}
			}
		}

		/// <summary>
		/// Check if an entity has a component.
		/// </summary>
		/// <typeparam name="TComponent">Type of component that may be attached to the entity.</typeparam>
		/// <param name="entity">Entity to check if component is attached.</param>
		/// <returns>`true` if the entity has a component of that type attached</returns>
		public bool ContainsComponent<TComponent>(Entity entity) where TComponent : IComponent
		{
			if (!_entities.ContainsKey(entity))
				return false;
			var entityComponents = _entities[entity];
			return entityComponents.ContainsKey(typeof(TComponent));
		}

		/// <summary>
		/// Gets the component attached to the entity.
		/// </summary>
		/// <typeparam name="TComponent">Type of component to remove.</typeparam>
		/// <param name="entity">Which entity owns the component?</param>
		/// <returns>`null` if no component is attached</returns>
		public TComponent ComponentFrom<TComponent>(Entity entity) where TComponent : class, IComponent
		{
			if (!_entities.ContainsKey(entity))
				return null;
			return _entities[entity][typeof(TComponent)] as TComponent;
		}

		/// <summary>
		/// Returns all entities that have the specified components.
		/// </summary>
		/// <returns>An enumerable list of entities, that will update automatically.</returns>
		public EntitySet EntitiesContaining(params Type[] types)
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
		/// Removes the component from the entity.
		/// </summary>
		/// <typeparam name="TComponent">Type of component to remove from entitiy.</typeparam>
		/// <param name="entity">Entity from which component is removed.</param>
		public void RemoveComponent<TComponent>(Entity entity) where TComponent : class, IComponent
		{
			var type = typeof(TComponent);
			if (!_entities.ContainsKey(entity))
				return;
			_entities[entity].Remove(type);

			// remove entity from any matchers that it no longer qualifies for
			foreach (var kvp in _matchers) {
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
	}
}