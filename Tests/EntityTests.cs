// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class EntityTests
	{
		[Test]
		public void Components()
		{
			var context = new Context();

			// init
			var e = context.CreateEntity();
			Assert.IsFalse(e.Has<AComponent>());

			// add
			var aComponent = new AComponent("a1");
			e.Add(aComponent);
			Assert.IsTrue(e.Has<AComponent>());
			Assert.AreSame(aComponent, e.Get<AComponent>());
			var bComponent = new BComponent();
			Assert.IsFalse(e.Has<BComponent>());
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
			Assert.IsFalse(e.Has<AComponent>());
		}
	}

	class AComponent : IComponent
	{
		private readonly string _name;

		public AComponent(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}

	class BComponent : IComponent
	{
	}
}