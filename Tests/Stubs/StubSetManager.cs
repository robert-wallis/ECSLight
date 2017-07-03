// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;

namespace Tests.Stubs
{
	internal class StubSetManager : ISetManager
	{
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			return new EntitySet(e => false);
		}

		public void RemoveSet(EntitySet set)
		{
		}

		public void UpdateSets(IEntity entity)
		{
		}
	}
}