// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using ECSLight;

namespace Tests
{
	internal class StubSetManager : ISetManager
	{
		public void AddEntityToSets(Entity entity, Type type)
		{
		}

		public EntitySet SetContaining(params Type[] types)
		{
			return null;
		}

		public void RemoveEntityFromSets(Entity entity, Type type)
		{
		}
	}
}