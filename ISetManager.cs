// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	public interface ISetManager
	{
		void AddEntityToAllSets(Entity entity);
		HashSet<Entity> SetContaining(params Type[] types);
		void RemoveEntityFromSets(Entity entity, Type type);
	}
}