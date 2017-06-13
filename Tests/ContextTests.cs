// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Linq;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	class ContextTests
	{
		[Test]
		public void CreateRemoveEntity()
		{
			var context = new Context();
			var e1 = context.CreateEntity();
			Assert.NotNull(e1);
			var e2 = context.CreateEntity();
			Assert.NotNull(e2);

			Assert.AreEqual(2, context.Count());

			context.ReleaseEntity(e1);
			Assert.AreEqual(1, context.Count());

			context.ReleaseEntity(e2);
			Assert.AreEqual(0, context.Count());
		}

		[Test]
		public void ContextsNotShared()
		{
			var c1 = new Context();
			var e1 = c1.CreateEntity();
			var c2 = new Context();
			var c1Count = 0;
			foreach (var e in c1) {
				Assert.AreEqual(e1, e);
				c1Count++;
			}
			var c2Count = 0;
			foreach (var e in c2) {
				Assert.Fail($"Should be no entities in c2, but there was {e}");
				// ReSharper disable once HeuristicUnreachableCode
				c2Count++;
			}
			Assert.AreEqual(1, c1Count);
			Assert.AreEqual(0, c2Count);
		}

		[Test]
		public void ContextSets()
		{
			var context = new Context();
			var entity = context.CreateEntity();
			var component = new AComponent("a1");
			// set doesn't match until component is attached
			{
				var setA = context.SetContaining(typeof(AComponent));
				Assert.AreEqual(0, setA.Count);
				entity.Add(component);
				// set should automatically match after component is added
				foreach (var e in setA) {
					Assert.AreEqual(component, e.Get<AComponent>());
				}
				Assert.AreEqual(1, setA.Count);
			}

			// B doesn't match 'entity' because it doesn't have a BComponent.
			{
				var setB = context.SetContaining(typeof(BComponent));
				Assert.AreEqual(0, setB.Count);
			}

			// another A set should also match
			{
				var setA2 = context.SetContaining(typeof(AComponent));
				foreach (var e in setA2) {
					Assert.AreEqual(component, e.Get<AComponent>());
				}
				Assert.AreEqual(1, setA2.Count);
			}

			// set should not increase the entities matched if another component is added
			{
				var setA = context.SetContaining(typeof(AComponent));
				Assert.AreEqual(1, setA.Count);
				entity.Add(new AComponent("inner"));
				Assert.AreEqual(1, setA.Count);
			}
		}

		[Test]
		public void ContextSetEdgeCases()
		{
			var context = new Context();
			var entity = context.CreateEntity();
			entity.Add(new AComponent("a"));
			entity.Add(new BComponent());
			var set = context.SetContaining(typeof(AComponent), typeof(BComponent));
			Assert.AreEqual(1, set.Count, "Set should only have 1 entity when it matches 2 components");
		}

		[Test]
		public void ContextSetRemoval()
		{
			var context = new Context();
			var set = context.SetContaining(typeof(AComponent));
			var entity = context.CreateEntity();
			var component = new AComponent("a1");
			entity.Add(component);

			// sanity check
			Assert.AreEqual(1, set.Count);
			Assert.AreSame(entity, set.First());
			Assert.AreSame(component, set.First().First());

			// remove entity, should remove components
			context.ReleaseEntity(entity);
			Assert.AreEqual(0, set.Count);
			Assert.IsNull(set.FirstOrDefault());
		}

		[Test]
		public void Coverage()
		{
			// cover the IEnumerable GetEnumerator function
			var context = new Context();
			context.CreateEntity();
			var enumerable = context as IEnumerable;
			foreach (var entity in enumerable) {
				Assert.IsNotNull(entity);
			}
		}
	}
}