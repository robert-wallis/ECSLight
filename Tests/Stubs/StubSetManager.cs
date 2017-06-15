// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubSetManager : ISetManager
	{
		public void AddEntityToAllSets(Entity entity)
		{
		}

		public HashSet<Entity> SetContaining(params Type[] types)
		{
			return null;
		}

		public void RemoveEntityFromSets(Entity entity, Type type)
		{
		}
	}
}