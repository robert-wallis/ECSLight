// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubEntityManager : IEntityManager
	{
		public string CreateEntity_Name;
		public IEntity CreateEntity_Return;
		public IEntity ReleaseEntity_Entity;
		public IEnumerator<IEntity> GetEnumerator_Return;

		public IEntity CreateEntity(string name = "")
		{
			CreateEntity_Name = name;
			return CreateEntity_Return;
		}

		public void ReleaseEntity(IEntity entity)
		{
			ReleaseEntity_Entity = entity;
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return GetEnumerator_Return;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}