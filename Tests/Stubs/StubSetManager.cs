// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;

namespace Tests.Stubs
{
	internal class StubSetManager : ISetManager
	{
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			return null;
		}

		public void RemoveSet(EntitySet set)
		{
		}

		public void ComponentAdded(IEntity entity, object component)
		{
		}

		public void ComponentReplaced(IEntity entity, object oldComponent, object component)
		{
		}

		public void ComponentRemoved(IEntity entity, object oldComponent)
		{
		}
	}
}