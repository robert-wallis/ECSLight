// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace ECSLight
{
	/// <summary>
	/// Keeps track of all the entity sets.
	/// Adds and removes entities to the appropriate sets.
	/// </summary>
	public interface ISetManager
	{
		/// <summary>
		/// Makes a new set and registers it for updating membership later.
		/// </summary>
		/// <returns>An enumerable list of entitySet, that will update automatically.</returns>
		EntitySet CreateSet(EntitySet.IncludeInSet predicate);

		/// <summary>
		/// Unregisters a set so it will no longer get membership updates.
		/// </summary>
		void RemoveSet(EntitySet set);

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		void ComponentAdded(IEntity entity, object component);

		/// <summary>
		/// Replace entity on matching sets, remove from unmatching sets.
		/// </summary>
		void ComponentReplaced(IEntity entity, object oldComponent, object component);

		/// <summary>
		/// Add entity to all matching sets, remove from any unmatching sets.
		/// </summary>
		void ComponentRemoved(IEntity entity, object oldComponent);

		/// <summary>
		/// Add or Remove components to this set when Components change.
		/// Undo a DisableSet.
		/// Default is enabled.
		/// </summary>
		void EnableSet(EntitySet set);

		/// <summary>
		/// Don't Add or Remove components to this set when Components change.
		/// Default state is enabled.
		/// </summary>
		/// <param name="set"></param>
		void DisableSet(EntitySet set);
	}
}