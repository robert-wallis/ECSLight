// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public class Context
	{
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly Dictionary<Type, Bag<IComponent>> _components;

		public Context()
		{
			_components = new Dictionary<Type, Bag<IComponent>>();
			_entities = new Dictionary<Entity, Dictionary<Type, IComponent>>(128);
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
		public void Add<TComponent>(Entity entity, TComponent component) where TComponent : class, IComponent
		{
			if (!_entities.ContainsKey(entity))
				_entities[entity] = new Dictionary<Type, IComponent>(1);
			if (!_components.ContainsKey(typeof(TComponent)))
				_components[typeof(TComponent)] = new Bag<IComponent>(128);
			_components[typeof(TComponent)].Add(component);
			_entities[entity][typeof(TComponent)] = component;
		}

		/// <summary>
		/// Check if an entity has a component.
		/// </summary>
		/// <typeparam name="TComponent">Type of component that may be attached to the entity.</typeparam>
		/// <param name="entity">Entity to check if component is attached.</param>
		/// <returns>`true` if the entity has a component of that type attached</returns>
		public bool Has<TComponent>(Entity entity) where TComponent : IComponent
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
		public TComponent Get<TComponent>(Entity entity) where TComponent : class, IComponent
		{
			if (!_entities.ContainsKey(entity))
				return null;
			return _entities[entity][typeof(TComponent)] as TComponent;
		}

		/// <summary>
		/// Returns all entities that have the specified components.
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns></returns>
		public ICollection<Entity> Having(params Type[] types)
		{
			if (!_components.ContainsKey(types[0]))
				return new List<Entity>();
			return (ICollection<Entity>)_components[types[0]];
		}

		/// <summary>
		/// Removes the component from the entity.
		/// </summary>
		/// <typeparam name="TComponent">Type of component to remove from entitiy.</typeparam>
		/// <param name="entity">Entity from which component is removed.</param>
		public void Remove<TComponent>(Entity entity) where TComponent : class, IComponent
		{
			if (!_entities.ContainsKey(entity))
				return;
			var component = Get<TComponent>(entity);
			if (component != null)
				_components[typeof(TComponent)].Remove(component);
			_entities[entity].Remove(typeof(TComponent));
		}


	}
}