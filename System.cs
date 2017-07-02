// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;
using System.Collections.Generic;

namespace ECSLight
{
	/// <summary>
	/// Systems run logic on entities.
	/// You sould subclass System to get easy API access to set management events.
	/// </summary>
	public abstract class System : IDisposable
	{
		/// <summary>
		/// If true, this System's sets will be updated.
		/// Your system subclass should check Enabled == true when executing.
		/// </summary>
		public bool Enabled
		{
			get { return _enabled; }
			set {
				_enabled = value;
				if (value)
					Enable();
				else
					Disable();
			}
		}

		private readonly ISetManager _setManager;
		private bool _enabled;
		private readonly List<EntitySet> _systemSets;

		public System(ISetManager setManager)
		{
			_setManager = setManager;
			_systemSets = new List<EntitySet>();
			Enabled = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Process your entities behavior in this function.
		/// Call TickIfEnabled() once per frame.
		/// </summary>
		protected abstract void Tick();

		public void TickIfEnabled()
		{
			if (Enabled)
				Tick();
		}

		/// <summary>
		/// Get an auto-updated set of entities.
		/// The set fires events for when an entity's component causes
		/// it to be added, replaced or removed from the set.
		/// </summary>
		/// <param name="predicate">
		///		Function that returns true if entity should be included.
		///		<example><![CDATA[e => e.Contains<MyComponent>()]]></example>
		/// </param>
		public EntitySet CreateSet(EntitySet.IncludeInSet predicate)
		{
			var set = _setManager.CreateSet(predicate);
			_systemSets.Add(set);
			return set;
		}

		/// <summary>
		/// Remove the previously created set from the system.
		/// Set will no longer be updated.
		/// </summary>
		public void RemoveSet(EntitySet set)
		{
			_systemSets.Remove(set);
			_setManager.RemoveSet(set);
		}

		private void Enable()
		{
			foreach (var set in _systemSets)
				_setManager.EnableSet(set);
		}

		private void Disable()
		{
			foreach (var set in _systemSets)
				_setManager.DisableSet(set);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing) {
				foreach (var set in _systemSets) {
					_setManager.RemoveSet(set);
				}
				_systemSets.Clear();
			}
		}
	}
}