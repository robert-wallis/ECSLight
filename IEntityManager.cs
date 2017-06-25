// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections.Generic;

namespace ECSLight
{
	public interface IEntityManager : IEnumerable<IEntity>
	{
		IEntity CreateEntity(string name = "");
		void ReleaseEntity(IEntity entity);
		void ReleaseAll();
	}
}