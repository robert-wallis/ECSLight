// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;
using NUnit.Framework;

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

			context.ReleaseEntity(e1);
			context.ReleaseEntity(e2);
		}

		[Test]
		public void ContextsNotShared()
		{
			var c1 = new Context();
			var e1 = c1.CreateEntity();
			var c2 = new Context();
			// TODO: enumerate components
		}

		[Test]
		public void Matchers()
		{
			var context = new Context();
			var entity = context.CreateEntity();
			var component = new AComponent("a1");
			// matcher doesn't match until component is attached
			{
				var matcher = context.SetContaining(typeof(AComponent));
				Assert.AreEqual(0, matcher.Count);
				entity.Add(component);
				// matcher should automatically match after component is added
				foreach (var e in matcher) {
					Assert.AreEqual(component, e.Get<AComponent>());
				}
				Assert.AreEqual(1, matcher.Count);
			}

			// B doesn't match 'entity' because it doesn't have a BComponent.
			{
				var matcherB = context.SetContaining(typeof(BComponent));
				Assert.AreEqual(0, matcherB.Count);
			}

			// another A matcher should also match
			{
				var matcherA2 = context.SetContaining(typeof(AComponent));
				foreach (var e in matcherA2) {
					Assert.AreEqual(component, e.Get<AComponent>());
				}
				Assert.AreEqual(1, matcherA2.Count);
			}
		}
	}
}