// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ComponentManagerTests
	{
		[Test]
		public void CoverEntityComponentsUnInitialized()
		{
			var entities = new Dictionary<Entity, Dictionary<Type, IComponent>>();
			var componentManager = new ComponentManager(entities, new StubSetManager());
			var entity = new Entity(componentManager);
			entity.Add(new AComponent("a"));
			Assert.IsNotNull(componentManager.ComponentFrom<AComponent>(entity));
		}

		[Test]
		public void CoverEntityNotInEntities()
		{
			var componentManager = new ComponentManager(new Dictionary<Entity, Dictionary<Type, IComponent>>(), new StubSetManager());
			var otherManager = new ComponentManager(new Dictionary<Entity, Dictionary<Type, IComponent>>(), new StubSetManager());
			var entity = new Entity(otherManager);
			Assert.IsFalse(componentManager.ContainsComponent(entity, typeof(AComponent)));
			Assert.IsNull(componentManager.ComponentFrom<AComponent>(entity));
			componentManager.RemoveComponent<AComponent>(entity);
		}
	}
}