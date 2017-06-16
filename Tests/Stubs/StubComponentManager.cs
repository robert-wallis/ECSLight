// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubComponentManager : IComponentManager
	{
		private static IEnumerator<IComponent> EmptyEnumerator = new List<IComponent>().GetEnumerator();

		public void AddComponent<TComponent>(IEntity entity, TComponent component) where TComponent : class, IComponent
		{
		}

		public TComponent ComponentFrom<TComponent>(IEntity entity) where TComponent : class, IComponent
		{
			return null;
		}

		public bool ContainsComponent<TComponent>(IEntity entity) where TComponent : IComponent
		{
			return false;
		}

		public bool ContainsComponent(IEntity entity, Type type)
		{
			return false;
		}

		public void RemoveComponent<TComponent>(IEntity entity) where TComponent : class, IComponent
		{
		}

		public void RemoveComponent(IEntity entity, Type type)
		{
		}

		public IEnumerator<IComponent> GetEnumerator(IEntity entity)
		{
			return EmptyEnumerator;
		}
	}
}