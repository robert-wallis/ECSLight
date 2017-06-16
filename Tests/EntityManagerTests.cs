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
		public void CoverageIEnumerable()
		{
			var entity = new StubEntity();
			var entityManager = new EntityManager(new List<IEntity> {entity}, new StubComponentManager());
			var enumerable = (IEnumerable)entityManager;
			var enumerator = enumerable.GetEnumerator();
			Assert.IsTrue(enumerator.MoveNext());
			Assert.AreSame(entity, enumerator.Current);
		}
	}
}