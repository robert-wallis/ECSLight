// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using ECSLight;

namespace Tests.Stubs
{
	internal class AComponent : IComponent
	{
		public string Name;

		public AComponent(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}