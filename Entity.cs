// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// An IEntity represents a single object that contains components.
	/// It's really just an interface to the ComponentManager for a single entity.
	/// Entities are not meant to be subclassed, please use
	///   Components to specify fields (data),
	///   and Systems to specify functions (behavior).
	/// </summary>
	public class Entity : IEntity
	{
		private readonly IEntityManager _entityManager;
		private readonly IComponentManager _componentManager;
		private readonly string _name;

		/// <summary>
		/// Please use Context.CreateEntity, don't subclass this.
		/// Please use Components for data, and Systems for behavior.
		/// Also to avoid GC, entities are pooled by Context.
		/// </summary>
		public Entity(IEntityManager entityManager, IComponentManager componentManager, string name = "")
		{
			_entityManager = entityManager;
			_componentManager = componentManager;
			_name = name;
		}

		/// <summary>
		/// End the lifecycle of this entity.
		/// </summary>
		public void Release()
		{
			_entityManager.ReleaseEntity(this);
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

		public override string ToString()
		{
			var components = string.Join(", ", this);
			return string.Format("{0}:[{1}]", _name, components);
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