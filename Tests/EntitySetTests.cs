// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using ECSLight;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class EntitySetTests
	{
		[Test]
		public void CoverIEnumerable()
		{
			var entitySet = new EntitySet();
			var entity = new Entity(new StubComponentManager());
			entitySet.Add(entity);
			var enumerable = entitySet as IEnumerable;
			foreach (var e in enumerable) {
				Assert.AreSame(entity, e);
			}
		}
	}
}