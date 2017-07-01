// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	public interface ISetManager
	{
		EntitySet CreateSet(EntitySet.IncludeInSet predicate);
		void RemoveSet(EntitySet set);
		void ComponentAdded(IEntity entity, object component);
		void ComponentReplaced(IEntity entity, object oldComponent, object component);
		void ComponentRemoved(IEntity entity, object oldComponent);
	}
}