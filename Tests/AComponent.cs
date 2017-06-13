// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;

namespace Tests
{
	class AComponent : IComponent
	{
		private readonly string _name;

		public AComponent(string name)
		{
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}
	}
}