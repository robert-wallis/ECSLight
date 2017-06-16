// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public interface IEntity : IEnumerable<IComponent>
	{
		/// <summary>
		/// End the lifecycle of this entity.
		/// </summary>
		void Release();

		/// <summary>
		/// Add a component to this entity.
		/// Updates context's entity sets.
		/// </summary>
		void Add<TComponent>(TComponent component) where TComponent : class, IComponent;

		/// <summary>
		/// Check if this entity has a component type.
		/// </summary>
		bool Contains<TComponent>() where TComponent : IComponent;

		/// <summary>
		/// Check if this entity has a component type.
		/// </summary>
		bool Contains(Type type);

		/// <summary>
		/// Get the component attached to this entity.
		/// </summary>
		/// <returns>null if the component is not attached</returns>
		TComponent Get<TComponent>() where TComponent : class, IComponent;

		/// <summary>
		/// Remove the component from this entity.
		/// Updates the context's entity sets.
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		void Remove<TComponent>() where TComponent : class, IComponent;
	}
}