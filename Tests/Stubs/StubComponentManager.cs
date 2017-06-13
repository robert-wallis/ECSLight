// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubComponentManager : IComponentManager
	{
		public void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : class, IComponent
		{
		}

		public TComponent ComponentFrom<TComponent>(Entity entity) where TComponent : class, IComponent
		{
			return null;
		}

		public bool ContainsComponent<TComponent>(Entity entity) where TComponent : IComponent
		{
			return false;
		}

		public bool ContainsComponent(Entity entity, Type type)
		{
			return false;
		}

		public void RemoveComponent<TComponent>(Entity entity) where TComponent : class, IComponent
		{
		}

		public void RemoveComponent(Entity entity, Type type)
		{
		}

		public IEnumerator<IComponent> GetEnumerator(Entity entity)
		{
			return null;
		}
	}
}