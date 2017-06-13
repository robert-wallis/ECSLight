// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class Entity : IEnumerable<IComponent>
	{
		private readonly IComponentManager _componentManager;

		/// <summary>
		/// Please use Context.CreateEntity, don't subclass this.
		/// Please use Components for data, and Systems for behavior.
		/// Also to avoid GC, entities are pooled by Context.
		/// </summary>
		public Entity(IComponentManager componentManager)
		{
			_componentManager = componentManager;
		}

		/// <summary>
		/// Add a component to this entity.
		/// Updates context's entity sets.
		/// </summary>
		public void Add<TComponent>(TComponent component) where TComponent : class, IComponent
		{
			_componentManager.AddComponent(this, component);
		}

		/// <summary>
		/// Check if this entity has a component type.
		/// </summary>
		public bool Contains<TComponent>() where TComponent : IComponent
		{
			return _componentManager.ContainsComponent<TComponent>(this);
		}

		/// <summary>
		/// Check if this entity has a component type.
		/// </summary>
		public bool Contains(Type type)
		{
			return _componentManager.ContainsComponent(this, type);
		}

		/// <summary>
		/// Get the component attached to this entity.
		/// </summary>
		/// <returns>null if the component is not attached</returns>
		public TComponent Get<TComponent>() where TComponent : class, IComponent
		{
			return _componentManager.ComponentFrom<TComponent>(this);
		}

		/// <summary>
		/// Remove the component from this entity.
		/// Updates the context's entity sets.
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		public void Remove<TComponent>() where TComponent : class, IComponent
		{
			_componentManager.RemoveComponent<TComponent>(this);
		}

		/// <summary>
		/// Enumerate through this entity's components.
		/// </summary>
		public IEnumerator<IComponent> GetEnumerator()
		{
			return _componentManager.GetEnumerator(this);
		}

		/// <summary>
		/// Enumerate through this entity's components.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}