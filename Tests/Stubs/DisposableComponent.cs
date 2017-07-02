// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System;

namespace Tests.Stubs
{
	internal class DisposableComponent : IDisposable
	{
		private readonly Action _dispose;

		public DisposableComponent(Action dispose)
		{
			_dispose = dispose;
		}
		public void Dispose()
		{
			_dispose?.Invoke();
		}
	}
}