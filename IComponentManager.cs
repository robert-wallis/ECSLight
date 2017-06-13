// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public interface IComponentManager
	{
		void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : class, IComponent;
		TComponent ComponentFrom<TComponent>(Entity entity) where TComponent : class, IComponent;
		bool ContainsComponent<TComponent>(Entity entity) where TComponent : IComponent;
		bool ContainsComponent(Entity entity, Type type);
		void RemoveComponent<TComponent>(Entity entity) where TComponent : class, IComponent;
		void RemoveComponent(Entity entity, Type type);
		IEnumerator<IComponent> GetEnumerator(Entity entity);
	}
}