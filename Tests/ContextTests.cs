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
			var e1 = context.CreateEntity("e1");
			Assert.NotNull(e1);
			var e2 = context.CreateEntity("e2");
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
			var e1 = c1.CreateEntity("e1");
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
			var entity = context.CreateEntity("original");
			var component = new AComponent("a1");
			// set doesn't match until component is attached
			var setA = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(AComponent)));
			{
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
				Assert.AreEqual(1, setA.Count);
				var setB = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(BComponent)));
				Assert.AreEqual(0, setB.Count);
			}

			// A doesn't match because it must be A AND B
			{
				Assert.AreEqual(1, setA.Count);
				Assert.IsFalse(entity.Contains<BComponent>());
				var setAandB = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(AComponent), typeof(BComponent)));
				Assert.AreNotSame(setA, setAandB);
				Assert.AreEqual(0, setAandB.Count, "Entity should not match, has no BComponent");
				var entityAandB = context.CreateEntity("ab");
				entityAandB.Add(new AComponent("ab test"));
				entityAandB.Add(new BComponent());
				Assert.AreEqual(1, setAandB.Count, "Entity should match, has A and B Components");
				Assert.AreEqual(2, setA.Count, "Set A should now match both");
				context.ReleaseEntity(entityAandB);
				Assert.AreEqual(0, setAandB.Count, "Entity should not match, was removed.");
			}

			// another A set should also match
			{
				var setA2 = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(AComponent)));
				foreach (var e in setA2) {
					Assert.AreEqual(component, e.Get<AComponent>());
				}
				Assert.AreEqual(1, setA2.Count);
			}

			// set should not increase the entities matched if another component is added
			{
				Assert.AreEqual(1, setA.Count);
				entity.Add(new AComponent("inner"));
				Assert.AreEqual(1, setA.Count);
			}
		}

		[Test]
		public void ContextSetEdgeCases()
		{
			var context = new Context();
			var entity = context.CreateEntity("edge");
			entity.Add(new AComponent("a"));
			entity.Add(new BComponent());
			var set = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(AComponent), typeof(BComponent)));
			Assert.AreEqual(1, set.Count, "Set should only have 1 entity when it matches 2 components");
		}

		[Test]
		public void ContextSetRemoval()
		{
			var context = new Context();
			var set = context.SetContaining(e => SetManager.EntityMatchesTypes(e, typeof(AComponent)));
			var entity = context.CreateEntity("set");
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
			context.CreateEntity("coverage");
			var enumerable = context as IEnumerable;
			foreach (var entity in enumerable) {
				Assert.IsNotNull(entity);
			}
		}
	}
}