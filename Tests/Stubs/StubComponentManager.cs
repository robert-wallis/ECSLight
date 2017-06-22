// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubComponentManager : IComponentManager
	{
		private static readonly IEnumerator<object> EmptyEnumerator = new List<object>().GetEnumerator();

		public void AddComponent<TComponent>(IEntity entity, TComponent component) where TComponent : class
		{
		}

		public TComponent ComponentFrom<TComponent>(IEntity entity) where TComponent : class
		{
			return null;
		}

		public bool ContainsComponent<TComponent>(IEntity entity)
		{
			return false;
		}

		public bool ContainsComponent(IEntity entity, Type type)
		{
			return false;
		}

		public void RemoveComponent<TComponent>(IEntity entity) where TComponent : class
		{
		}

		public void RemoveComponent(IEntity entity, Type type)
		{
		}

		public IEnumerator<object> GetEnumerator(IEntity entity)
		{
			return EmptyEnumerator;
		}
	}
}