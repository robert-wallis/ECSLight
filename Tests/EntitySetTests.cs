// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
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
			entitySet.Add(entity);
			Assert.AreEqual(1, entitySet.Count);
			entitySet.Remove(entity);
			Assert.AreEqual(0, entitySet.Count);
			entitySet.Add(entity);
			Assert.AreEqual(1, entitySet.Count);
			entitySet.Clear();
			Assert.AreEqual(0, entitySet.Count);
		}

		[Test]
		public void Events()
		{
			var addCount = 0;
			var removeCount = 0;
			var entity = new StubEntity();
			var entitySet = new EntitySet(e => true);
			entitySet.OnAdded += e => {
				Assert.AreSame(entity, e);
				addCount++;
			};
			entitySet.OnRemoved += e => {
				Assert.AreSame(entity, e);
				removeCount++;
			};
			Assert.AreEqual(0, addCount);
			Assert.AreEqual(0, removeCount);
			entitySet.Add(entity);
			Assert.AreEqual(1, addCount);
			Assert.AreEqual(0, removeCount);
			entitySet.Remove(entity);
			Assert.AreEqual(1, addCount);
			Assert.AreEqual(1, removeCount);
			entitySet.Add(entity);
			Assert.AreEqual(2, addCount);
			Assert.AreEqual(1, removeCount);
			entitySet.Clear();
			Assert.AreEqual(2, addCount);
			Assert.AreEqual(2, removeCount);
		}

		[Test]
		public void EventUselessBox()
		{
			var context = new Context();
			var entity = context.CreateEntity("test entity");
			var set = context.CreateSet(e => e.Contains<AComponent>());
			set.OnRemoved += e => {
				e.Add(new AComponent("back on"));
			};
			entity.Add(new AComponent("first on"));
			entity.Remove<AComponent>();
			Assert.AreEqual("back on", entity.Get<AComponent>().Name, "A should be replaced with inner");
		}

		[Test]
		public void CoverageICollection()
		{
			var entitySet = new EntitySet(e => true);
			var entity = new StubEntity();
			entitySet.Add(entity);

			// Contains
			Assert.IsTrue(entitySet.Contains(entity));

			// GetEnumerator
			Assert.AreSame(entity, entitySet.First());
			var enumerable = (IEnumerable)entitySet;
			var enumerator = enumerable.GetEnumerator();
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreSame(entity, enumerator.Current);

			// CopyTo
			var array = new IEntity[1];
			((ICollection<IEntity>)entitySet).CopyTo(array, 0);
			Assert.AreSame(entity, array.First());

			// IsReadOnly
			var collection = (ICollection<IEntity>)entitySet;
			Assert.False(collection.IsReadOnly);
		}
	}
}