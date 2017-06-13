// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	public class Entity
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

		public void Add<TComponent>(TComponent component) where TComponent : class, IComponent
		{
			_componentManager.AddComponent(this, component);
		}

		public bool Contains<TComponent>() where TComponent : IComponent
		{
			return _componentManager.ContainsComponent<TComponent>(this);
		}

		public TComponent Get<TComponent>() where TComponent : class, IComponent
		{
			return _componentManager.ComponentFrom<TComponent>(this);
		}

		public void Remove<TComponent>() where TComponent : class, IComponent
		{
			_componentManager.RemoveComponent<TComponent>(this);
		}
	}
}