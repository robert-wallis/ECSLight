// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubEntityManager : IEntityManager
	{
		public IEnumerator<IEntity> GetEnumeratorReturn = new List<IEntity>().GetEnumerator();

		public IEntity CreateEntity(string name = "")
		{
			return null;
		}

		public void ReleaseEntity(IEntity entity)
		{
		}

		public void ReleaseAll()
		{
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return GetEnumeratorReturn;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}