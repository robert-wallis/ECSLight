// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	public class ComponentManagerTests
	{
		readonly IEntityManager _stubEntityManager = new StubEntityManager();

		[Test]
		public void CoverEntityComponentsUnInitialized()
		{
			var componentManager = new ComponentManager(new StubSetManager());
			var entity = new Entity(_stubEntityManager, componentManager) {
				new AComponent("a")
			};
			Assert.IsNotNull(componentManager.ComponentFrom<AComponent>(entity));
		}

		[Test]
		public void CoverEntityNotInEntities()
		{
			var componentManager = new ComponentManager(new StubSetManager());
			var otherManager = new ComponentManager(new StubSetManager());
			var entity = new Entity(_stubEntityManager, otherManager);
			Assert.IsFalse(componentManager.ContainsComponent(entity, typeof(AComponent)));
			Assert.IsNull(componentManager.ComponentFrom<AComponent>(entity));
			componentManager.RemoveComponent<AComponent>(entity);
		}

		[Test]
		public void CoverIEnumerable()
		{
			var componentManager = new ComponentManager(new StubSetManager());
			using (var enumerator = componentManager.GetEnumerator(new StubEntity())) {
				Assert.IsFalse(enumerator.MoveNext());
			}
			// check again for dispose crash
			using (var enumerator = componentManager.GetEnumerator(new StubEntity())) {
				Assert.IsFalse(enumerator.MoveNext());
			}
		}
	}
}