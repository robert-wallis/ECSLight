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
			var matcher = context.Having(typeof(AComponent));
			Assert.AreEqual(0, matcher.Count);
			var component = new AComponent("a1");
			entity.Add(component);
			var enumerator = matcher.GetEnumerator();
			enumerator.MoveNext();
			Assert.AreEqual(component, enumerator.Current);
		}
	}
}