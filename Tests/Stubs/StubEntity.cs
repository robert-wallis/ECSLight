// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubEntity : IEntity
	{
		public IEnumerator<IComponent> GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
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