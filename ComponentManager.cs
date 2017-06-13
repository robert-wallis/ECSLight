// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public class ComponentManager : IComponentManager
	{
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly ISetManager _setManager;

		public ComponentManager(Dictionary<Entity, Dictionary<Type, IComponent>> entities, ISetManager setManager)
		{
			_entities = entities;
			_setManager = setManager;
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
			_setManager.AddEntityToSets(entity, type);
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
			_setManager.RemoveEntityFromSets(entity, type);
		}
	}
}