// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;

namespace ECSLight
{
	public interface ISetManager
	{
		EntitySet CreateSet(Predicate<IEntity> predicate);
		void RemoveSet(EntitySet set);
		void ComponentAdded(IEntity entity, IComponent component);
		void ComponentReplaced(IEntity entity, IComponent old, IComponent component);
		void ComponentRemoved(IEntity entity, IComponent component);
	}
}