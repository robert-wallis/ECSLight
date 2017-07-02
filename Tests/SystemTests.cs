// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	class SystemTests
	{
		[Test]
		public void EnableDisable()
		{
			var context = new Context();
			var system = new TestSystem(context.SetManager);
			var tickCount = 0;
			var addCount = 0;
			system.OnTick += () => tickCount++;
			system.OnAdded += () => addCount++;
			Assert.AreEqual(true, system.Enabled, "System should be Enabled by default.");
			system.TickIfEnabled();
			Assert.AreEqual(1, tickCount, "Enabled systems should tick.");

			// WHEN a matching component is added
			{
				var entity = context.CreateEntity("testy");
				entity.Add(new AComponent("ac"));
			}

			// THEN it should send events
			Assert.AreEqual(1, addCount, "Enabled systems should send events");

			// WHEN the system is disabled
			system.Enabled = false;

			// THEN tick should not work
			system.TickIfEnabled();
			Assert.AreEqual(1, tickCount, "Disabled systems should not tick.");

			// THEN the event should not work
			{
				var entity = context.CreateEntity("testy 2");
				entity.Add(new AComponent("ac2"));
			}
			Assert.AreEqual(1, addCount, "Disabled systems should not send events.");

			// WHEN the system is re-enabled
			system.Enabled = true;

			// THEN events should have fired when it was enabled
			Assert.AreEqual(2, addCount, "Enabled systems should send events");

			// THEN tick should tick
			system.TickIfEnabled();
			Assert.AreEqual(2, tickCount, "Enabled systems should tick.");
		}

		[Test]
		public void Dispose()
		{
			// GIVEN a system
			var context = new Context();
			var tickCount = 0;
			var addCount = 0;
			using (var system = new TestSystem(context.SetManager)) {
				system.OnTick += () => tickCount++;
				system.OnAdded += () => addCount++;
				// WHEN it is disposed
			}
			// THEN it shouldn't fire events
			var entity = context.CreateEntity("dispose test");
			entity.Add(new AComponent("ac"));
			Assert.AreEqual(0, tickCount);
			Assert.AreEqual(0, addCount);
		}

		[Test]
		public void RemoveSet()
		{
			// GIVEN a system
			var context = new Context();
			var tickCount = 0;
			var addCount = 0;
			var system = new TestSystem(context.SetManager);
			system.OnTick += () => tickCount++;
			system.OnAdded += () => addCount++;

			// WHEN a set is removed
			system.TestRemoveSet();

			// THEN it shouldn't fire events
			var entity = context.CreateEntity("dispose test");
			entity.Add(new AComponent("ac"));
			Assert.AreEqual(0, tickCount);
			Assert.AreEqual(0, addCount);
		}
	}

	internal class TestSystem : ECSLight.System
	{
		private EntitySet _set;
		public event Action OnTick;
		public event Action OnAdded;

		public TestSystem(ISetManager setManager) : base(setManager)
		{
			_set = CreateSet(e => e.Contains<AComponent>());
			_set.OnAdded += (e, n) => OnAdded?.Invoke();
		}

		public void TestRemoveSet()
		{
			RemoveSet(_set);
			_set = null;
		}

		protected override void Tick()
		{
			OnTick?.Invoke();
		}
	}
}