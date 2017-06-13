// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;

namespace ECSLight
{
	public interface ISetManager
	{
		void AddEntityToSets(Entity entity, Type type);
		EntitySet SetContaining(params Type[] types);
		void RemoveEntityFromSets(Entity entity, Type type);
	}
}