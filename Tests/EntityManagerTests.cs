// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using ECSLight;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
	[TestFixture]
	public class EntityManagerTests
	{
		[Test]
		public void ModifiedEnumerable()
		{
			var entity = new StubEntity();
			var list = new List<IEntity> { entity };
			var entityManager = new EntityManager(list, new StubComponentManager());
			var enumerable = (IEnumerable) entityManager;
			var enumerator = enumerable.GetEnumerator();
			list.Add(new StubEntity()); // modify collection
			Assert.IsTrue(enumerator.MoveNext(), "should iterate over original entity list");
			Assert.AreSame(entity, enumerator.Current);
			list.Add(new StubEntity()); // modify again
			Assert.IsFalse(enumerator.MoveNext(), "should only have one entity in original list");
		}
	}
}