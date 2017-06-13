// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	public interface IComponentManager
	{
		void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : class, IComponent;
		TComponent ComponentFrom<TComponent>(Entity entity) where TComponent : class, IComponent;
		bool ContainsComponent<TComponent>(Entity entity) where TComponent : IComponent;
		void RemoveComponent<TComponent>(Entity entity) where TComponent : class, IComponent;
	}
}