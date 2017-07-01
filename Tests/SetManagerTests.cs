// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections.Generic;
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
			var setManager = new SetManager(entities);
			var componentManager = new ComponentManager(setManager);
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
	}
}