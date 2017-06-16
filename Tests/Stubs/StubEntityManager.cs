// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubEntityManager : IEntityManager
	{
		public string CreateEntity_Name;
		public Entity CreateEntity_Return;
		public Entity ReleaseEntity_Entity;
		public IEnumerator<Entity> GetEnumerator_Return;

		public Entity CreateEntity(string name = "")
		{
			CreateEntity_Name = name;
			return CreateEntity_Return;
		}

		public void ReleaseEntity(Entity entity)
		{
			ReleaseEntity_Entity = entity;
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return GetEnumerator_Return;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}