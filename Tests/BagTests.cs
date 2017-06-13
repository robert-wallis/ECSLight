// Copyright (C) 2017 Robert A. Wallis, All Rights Reserved.

using System.Collections;
using System.Linq;
using ECSLight;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class BagTests
	{
		[Test]
		public void AddRemove()
		{
			var bag = new Bag<int>();
			bag.Add(1);
			bag.Add(2);
			bag.Add(3);
			Assert.AreEqual(6, bag.Sum());
			Assert.IsTrue(bag.Remove(2));
			Assert.AreEqual(4, bag.Sum());
			bag.Remove(1);
			bag.Remove(3);
			Assert.AreEqual(0, bag.Sum());
			Assert.IsFalse(bag.Remove(1)); // doesn't exist
		}

		[Test]
		public void CoverageICollection()
		{
			var bag = new Bag<int>();
			bag.Add(1);
			bag.Add(2);
			bag.Add(3);
			var array = new int[3];
			bag.CopyTo(array, 0);
			Assert.AreEqual(6, array.Sum());

			var enumerable = bag as IEnumerable;
			var sum = 0;
			foreach(var i in enumerable) {
				sum += (int)i;
			}
			Assert.AreEqual(6, sum);

			Assert.IsTrue(bag.Contains(2));
			bag.Clear();
			Assert.IsFalse(bag.Contains(2));

			Assert.IsFalse(bag.IsReadOnly);
		}
	}
}