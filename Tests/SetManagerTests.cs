// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections.Generic;
using System.Linq;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	public class SetManagerTests
	{
		[Test]
		public void CreateRemove()
		{
			var entities = new List<IEntity>();
			var setManager = (ISetManager) new SetManager(entities);
			var componentManager = (IComponentManager) new ComponentManager(setManager);
			var entity = new Entity(new StubEntityManager(), componentManager, "test entity") {
				new AComponent("ac")
			};
			entities.Add(entity);
			var set = setManager.CreateSet(e => e.Contains<AComponent>());
			Assert.IsTrue(set.Matches(entity));
			Assert.AreEqual(1, set.Count, "set should have added existing entity");

			entity.Remove<AComponent>();
			Assert.IsFalse(set.Matches(entity));
			Assert.AreEqual(0, set.Count, "set should have been updated by set manager");

			setManager.RemoveSet(set);
			entity.Add(new AComponent("ac2"));
			Assert.IsTrue(set.Matches(entity));
			Assert.AreEqual(0, set.Count, "set shouldn't have been updated");
		}

		[Test]
		public void MissingComponentPredicate()
		{
			var entities = new List<IEntity>();
			var setManager = (ISetManager) new SetManager(entities);
			var componentManager = (IComponentManager) new ComponentManager(setManager);
			var entity = new Entity(new StubEntityManager(), componentManager, "test entity");
			entities.Add(entity);
			var set = setManager.CreateSet(e => !e.Contains<AComponent>());
			Assert.IsTrue(set.Matches(entity));
			Assert.AreEqual(1, set.Count, "set should have added entity");

			entity.Add(new AComponent("ac1"));
			Assert.IsFalse(set.Matches(entity));
			Assert.AreEqual(0, set.Count, "set should have been updated by set manager");

			setManager.RemoveSet(set);
			entity.Remove<AComponent>();
			Assert.IsTrue(set.Matches(entity));
			Assert.AreEqual(0, set.Count, "set shouldn't have been updated");
		}

		[Test]
		public void EnableDisableSet()
		{
			// GIVEN a disabled set of entities
			var entities = new List<IEntity>();
			var setManager = (ISetManager) new SetManager(entities);
			var componentManger = (IComponentManager) new ComponentManager(setManager);
			var entity = new Entity(new StubEntityManager(), componentManger, "test entity");
			entities.Add(entity);
			var set = setManager.CreateSet(e => e.Contains<AComponent>());
			Assert.AreEqual(0, set.Count, "sanity check");
			var addCount = 0;
			var removeCount = 0;
			set.OnAdded += (e, n) => addCount++;
			set.OnRemoved += (e, o) => removeCount++;
			setManager.DisableSet(set);

			// WHEN a component that would normally match is added or removed
			entity.Add(new AComponent("first"));
			entity.Remove<AComponent>();
			entity.Add(new AComponent("wait for it"));

			// THEN the events shouldn't fire
			Assert.AreEqual(0, addCount, "OnAdded shouldn't fire on a disabled set.");
			Assert.AreEqual(0, removeCount, "OnRemoved shouldn't fire on a disabled set.");

			// WHEN the set is re-enabled
			setManager.EnableSet(set);

			// THEN the Add event should fire
			Assert.AreEqual(1, addCount, "When a set is enabled, it's entities should be updated.");
			Assert.AreEqual(0, removeCount, "Shouldn't be removed from posititive set.");
			Assert.AreEqual(1, set.Count, "entity should have AComponent, and should be in set");
			Assert.AreSame(set.First(), entity);
		}

		[Test]
		public void EnableDisableNegativeSet()
		{
			// GIVEN a disabled set of entities
			var entities = new List<IEntity>();
			var setManager = (ISetManager) new SetManager(entities);
			var componentManger = (IComponentManager) new ComponentManager(setManager);
			var entity = new Entity(new StubEntityManager(), componentManger, "test entity");
			entities.Add(entity);
			var nSet = setManager.CreateSet(e => !e.Contains<AComponent>());
			Assert.AreEqual(1, nSet.Count, "sanity check");
			var nAddCount = 0;
			var nRemoveCount = 0;
			nSet.OnAdded += (e, n) => nAddCount++;
			nSet.OnRemoved += (e, o) => nRemoveCount++;
			setManager.DisableSet(nSet);

			// WHEN a component that would normally match is added or removed
			entity.Add(new AComponent("first"));
			entity.Remove<AComponent>();
			entity.Add(new AComponent("wait for it"));

			// THEN the events shouldn't fire
			Assert.AreEqual(0, nAddCount, "OnAdded shouldn't fire on the negative disabled set.");
			Assert.AreEqual(0, nRemoveCount, "OnRemoved shouldn't fire on the negative disabled set.");

			// WHEN the set is re-enabled
			setManager.EnableSet(nSet);

			// THEN the Add event should fire
			Assert.AreEqual(0, nAddCount, "Shouldn't be added to negative set, even though component is different.");
			Assert.AreEqual(1, nRemoveCount, "Should have been removed from negative set");
			Assert.AreEqual(0, nSet.Count, "entity should not have AComponent, so shouldn't be in set");
		}
	}
}