// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

namespace Tests.Stubs
{
	internal class AComponent
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