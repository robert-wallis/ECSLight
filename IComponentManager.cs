// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public interface IComponentManager
	{
		void AddComponent<TComponent>(IEntity entity, TComponent component) where TComponent : class;
		TComponent ComponentFrom<TComponent>(IEntity entity) where TComponent : class;
		bool ContainsComponent<TComponent>(IEntity entity);
		bool ContainsComponent(IEntity entity, Type type);
		void RemoveComponent<TComponent>(IEntity entity) where TComponent : class;
		void RemoveComponent(IEntity entity, Type type);
		IEnumerator<object> GetEnumerator(IEntity entity);
	}
}