// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Linq;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	class EntitySetTests
	{
		[Test]
		public void AddRemove()
		{
			var entitySet = new EntitySet(e => true);
			var entity = new StubEntity();
			entitySet.Add(entity, null);
			Assert.AreEqual(1, entitySet.Count);
			entitySet.Remove(entity, null);
			Assert.AreEqual(0, entitySet.Count);
			entitySet.Add(entity, null);
			Assert.AreEqual(1, entitySet.Count);
		}

		[Test]
		public void Events()
		{
			var addCount = 0;
			var removeCount = 0;
			var entity = new StubEntity();
			var entitySet = new EntitySet(e => true);
			entitySet.OnAdded += (e, n) =>
			{
				Assert.AreSame(entity, e);
				addCount++;
			};
			entitySet.OnRemoved += (e, o) =>
			{
				Assert.AreSame(entity, e);
				removeCount++;
			};
			Assert.AreEqual(0, addCount);
			Assert.AreEqual(0, removeCount);
			entitySet.Add(entity, null);
			Assert.AreEqual(1, addCount);
			Assert.AreEqual(0, removeCount);
			entitySet.Remove(entity, null);
			Assert.AreEqual(1, addCount);
			Assert.AreEqual(1, removeCount);
			entitySet.Add(entity, null);
			Assert.AreEqual(2, addCount);
			Assert.AreEqual(1, removeCount);
		}

		[Test]
		public void EventUselessBox()
		{
			{
				var context = new Context();
				var entity = context.CreateEntity("add remove");
				var set = context.CreateSet(e => e.Contains<AComponent>());
				set.OnAdded += (e, n) => { e.Remove<AComponent>(); };
				entity.Add(new AComponent("on"));
				Assert.IsFalse(entity.Contains<AComponent>(), "Set should remove A components.");
			}
			{
				var context = new Context();
				var entity = context.CreateEntity("test entity");
				var set = context.CreateSet(e => e.Contains<AComponent>());
				set.OnRemoved += (e, o) => { e.Add(new AComponent("back on")); };
				entity.Add(new AComponent("first on"));
				entity.Remove<AComponent>();
				Assert.AreEqual("back on", entity.Get<AComponent>().Name, "A should be replaced with inner");
			}
		}

		[Test]
		public void CoverageIEnumerable()
		{
			var entitySet = new EntitySet(e => true);
			var entity = new StubEntity();
			entitySet.Add(entity, null);

			// Contains
			Assert.IsTrue(entitySet.Contains(entity));

			// GetEnumerator
			Assert.AreSame(entity, entitySet.First());
			var enumerable = (IEnumerable) entitySet;
			var enumerator = enumerable.GetEnumerator();
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreSame(entity, enumerator.Current);
		}

	}
}