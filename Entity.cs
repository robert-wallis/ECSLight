// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	public class Entity
	{
		private readonly Context _context;

		/// <summary>
		/// Please use Context.CreateEntity, don't subclass this.
		/// Please use Components for data, and Systems for behavior.
		/// Also to avoid GC, entities are pooled by Context.
		/// </summary>
		public Entity(Context context)
		{
			_context = context;
		}

		public void Add<TComponent>(TComponent component) where TComponent : class, IComponent
		{
			_context.AddComponent(this, component);
		}

		public bool Contains<TComponent>() where TComponent : IComponent
		{
			return _context.ContainsComponent<TComponent>(this);
		}

		public TComponent Get<TComponent>() where TComponent : class, IComponent
		{
			return _context.ComponentFrom<TComponent>(this);
		}

		public void Remove<TComponent>() where TComponent : class, IComponent
		{
			_context.RemoveComponent<TComponent>(this);
		}
	}
}