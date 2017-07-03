// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	public interface ISetManager
	{
		EntitySet CreateSet(EntitySet.IncludeInSet predicate);
		void RemoveSet(EntitySet set);
		void UpdateSets(IEntity entity);
	}
}