// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	public class EntityTests
	{
		[Test]
		public void CreateRemove()
		{
			var componentManager = new StubComponentManager();
			var entityManager = new StubEntityManager();
			var entity = new Entity(entityManager, componentManager);
			entity.Release();
		}

		[Test]
		public void Components()
		{
			var context = new Context();

			// init
			var e = context.CreateEntity("e");
			Assert.IsFalse(e.Contains<AComponent>());
			Assert.IsFalse(e.Contains(typeof(AComponent)));

			// add
			var aComponent = new AComponent("a1");
			e.Add(aComponent);
			Assert.IsTrue(e.Contains<AComponent>());
			Assert.IsTrue(e.Contains(typeof(AComponent)));
			Assert.AreSame(aComponent, e.Get<AComponent>());
			var bComponent = new BComponent();
			Assert.IsFalse(e.Contains<BComponent>());
			e.Add(bComponent);
			Assert.AreSame(bComponent, e.Get<BComponent>());
			Assert.AreSame(aComponent, e.Get<AComponent>());

			// replace
			var aComponent2 = new AComponent("a2");
			Assert.AreNotSame(aComponent2, e.Get<AComponent>());
			e.Add(aComponent2);
			Assert.AreSame(aComponent2, e.Get<AComponent>());

			// remove
			e.Remove<AComponent>();
			Assert.IsFalse(e.Contains<AComponent>());
			Assert.IsNull(e.Get<AComponent>());
		}

		[Test]
		public void CoverIEnumerable()
		{
			var entity = new Entity(new StubEntityManager(), new StubComponentManager());
			var enumerable = (IEnumerable) entity;
			enumerable.GetEnumerator();
			// didn't crash
		}

		[Test]
		public void TestToString()
		{
			var entityManager = new StubEntityManager();
			var componentManager = new ComponentManager(new StubSetManager());
			var entity = new Entity(entityManager, componentManager, "entity name") {
				new AComponent("component name")
			};
			var str = entity.ToString();
			Assert.IsTrue(str.Contains("entity name"), str);
			Assert.IsTrue(str.Contains("component name"), str);
		}
	}
}