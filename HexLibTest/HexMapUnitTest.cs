using System;
using HexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexLibTest
{
	[TestClass]
	public class HexMapUnitTest
	{
		[TestMethod]
		public void ShouldFailToCreateMapWithNegativeRadius()
		{
			Assert.ThrowsException<ArgumentException>(() => { new HexMap<string>(-1); });
		}

		[TestMethod]
		public void ShouldSuccessToCreateMapWithPositiveRadius()
		{
			IHexMap map;

			map = new HexMap<string>(0);
			Assert.AreEqual(0, map.Radius);
			Assert.AreEqual(1, map.Count);

			map = new HexMap<string>(1);
			Assert.AreEqual(1, map.Radius);
			Assert.AreEqual(7, map.Count);

			map = new HexMap<string>(2);
			Assert.AreEqual(2, map.Radius);
			Assert.AreEqual(19, map.Count);

			map = new HexMap<string>(3);
			Assert.AreEqual(3, map.Radius);
			Assert.AreEqual(37, map.Count);
		}

		[TestMethod]
		public void ShouldReturnCorrectPerimeter()
		{
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetPerimeter(-1); });
			Assert.AreEqual(1, HexMap.GetPerimeter(0));
			Assert.AreEqual(6, HexMap.GetPerimeter(1));
			Assert.AreEqual(12, HexMap.GetPerimeter(2));
			Assert.AreEqual(18, HexMap.GetPerimeter(3));
		}

		[TestMethod]
		public void ShouldReturnCorrectCount()
		{
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetPerimeter(-1); });
			Assert.AreEqual(1, HexMap.GetCount(0));
			Assert.AreEqual(7, HexMap.GetCount(1));
			Assert.AreEqual(19, HexMap.GetCount(2));
			Assert.AreEqual(37, HexMap.GetCount(3));
		}



	

	}
}