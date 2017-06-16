// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public interface ISetManager
	{
		EntitySet SetContaining(Predicate<Entity> predicate);
		void UpdateEntityMembership(Entity entity);
	}
}