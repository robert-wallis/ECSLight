// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubEntity : IEntity
	{
		private static IEnumerator<IComponent> Empty = new List<IComponent>().GetEnumerator();

		public IEnumerator<IComponent> GetEnumerator()
		{
			return Empty;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Empty;
		}

		public void Release()
		{
		}

		public void Add<TComponent>(TComponent component) where TComponent : class, IComponent
		{
		}

		public bool Contains<TComponent>() where TComponent : IComponent
		{
			return false;
		}

		public bool Contains(Type type)
		{
			return false;
		}

		public TComponent Get<TComponent>() where TComponent : class, IComponent
		{
			return null;
		}

		public void Remove<TComponent>() where TComponent : class, IComponent
		{
		}
	}
}