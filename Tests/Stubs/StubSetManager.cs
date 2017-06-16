// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;
using ECSLight;

namespace Tests.Stubs
{
	internal class StubSetManager : ISetManager
	{
		public HashSet<Entity> SetContainingReturn;
		public Predicate<Entity> SetContainingPredicate;
		public Entity UpdateEntityMembershipEntity;

		public HashSet<Entity> SetContaining(Predicate<Entity> predicate)
		{
			SetContainingPredicate = predicate;
			return SetContainingReturn;
		}

		public void UpdateEntityMembership(Entity entity)
		{
			UpdateEntityMembershipEntity = entity;
		}
	}
}