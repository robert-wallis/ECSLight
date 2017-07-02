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
			entitySet.Add(entity);
			Assert.AreEqual(1, entitySet.Count);
			entitySet.Remove(entity);
			Assert.AreEqual(0, entitySet.Count);
			entitySet.Add(entity);
			Assert.AreEqual(1, entitySet.Count);
		}

		[Test]
		public void CoverageIEnumerable()
		{
			var entitySet = new EntitySet(e => true);
			var entity = new StubEntity();
			entitySet.Add(entity);

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