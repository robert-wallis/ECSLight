// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubSetManager : ISetManager
	{
		public EntitySet CreateSetReturn;
		public Predicate<IEntity> CreateSetPredicate;
		public EntitySet RemoveSetSet;

		public EntitySet CreateSet(Predicate<IEntity> predicate)
		{
			CreateSetPredicate = predicate;
			return CreateSetReturn;
		}

		public void RemoveSet(EntitySet set)
		{
			RemoveSetSet = set;
		}

		public void ComponentAdded(IEntity entity, IComponent component)
		{
		}

		public void ComponentReplaced(IEntity entity, IComponent old, IComponent component)
		{
		}

		public void ComponentRemoved(IEntity entity, IComponent component)
		{
		}
	}
}