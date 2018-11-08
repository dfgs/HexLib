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

			for (int t = 0; t < 3; t++)
			{
				map = new HexMap<string>(t);
				Assert.AreEqual(t, map.Radius);
			}
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
		public void ShouldReturnCorrectModuloRingIndex()
		{
			// radius = -1
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetModuloRingIndex(-1, 0); });

			// radius = 0
			for (int t = -10; t < 10; t++) Assert.AreEqual(0, HexMap.GetModuloRingIndex(0, t));

			// radius = 1
			for (int t = 0; t < 6; t++) Assert.AreEqual(t, HexMap.GetModuloRingIndex(1, t));
			for (int t = 6; t < 12; t++) Assert.AreEqual(t - 6, HexMap.GetModuloRingIndex(1, t));
			Assert.AreEqual(5, HexMap.GetModuloRingIndex(1, -1));
			Assert.AreEqual(4, HexMap.GetModuloRingIndex(1, -2));
			Assert.AreEqual(3, HexMap.GetModuloRingIndex(1, -3));
			Assert.AreEqual(2, HexMap.GetModuloRingIndex(1, -4));
			Assert.AreEqual(1, HexMap.GetModuloRingIndex(1, -5));

			// radius = 2
			for (int t = 0; t < 12; t++) Assert.AreEqual(t, HexMap.GetModuloRingIndex(2, t));
			for (int t = 12; t < 24; t++) Assert.AreEqual(t - 12, HexMap.GetModuloRingIndex(2, t));
			Assert.AreEqual(11, HexMap.GetModuloRingIndex(2, -1));
			Assert.AreEqual(10, HexMap.GetModuloRingIndex(2, -2));
			Assert.AreEqual(9, HexMap.GetModuloRingIndex(2, -3));
			Assert.AreEqual(8, HexMap.GetModuloRingIndex(2, -4));
			Assert.AreEqual(7, HexMap.GetModuloRingIndex(2, -5));


		}
		[TestMethod]
		public void ShouldReturnCorrectIndex()
		{
			// radius = -1
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetIndex(-1, 0); });

			// radius = 0
			for (int t = -10; t < 10; t++) Assert.AreEqual(0, HexMap.GetIndex(0, t));

			Assert.AreEqual(1, HexMap.GetIndex(1, 0));
			Assert.AreEqual(2, HexMap.GetIndex(1, 1));
			Assert.AreEqual(3, HexMap.GetIndex(1, 2));
			Assert.AreEqual(4, HexMap.GetIndex(1, 3));
			Assert.AreEqual(5, HexMap.GetIndex(1, 4));
			Assert.AreEqual(6, HexMap.GetIndex(1, 5));
			Assert.AreEqual(1, HexMap.GetIndex(1, 6));

			Assert.AreEqual(7, HexMap.GetIndex(2, 0));
			Assert.AreEqual(8, HexMap.GetIndex(2, 1));
			Assert.AreEqual(9, HexMap.GetIndex(2, 2));
			Assert.AreEqual(10, HexMap.GetIndex(2, 3));
			Assert.AreEqual(11, HexMap.GetIndex(2, 4));
			Assert.AreEqual(12, HexMap.GetIndex(2, 5));
			Assert.AreEqual(13, HexMap.GetIndex(2, 6));
			Assert.AreEqual(14, HexMap.GetIndex(2, 7));
			Assert.AreEqual(15, HexMap.GetIndex(2, 8));
			Assert.AreEqual(16, HexMap.GetIndex(2, 9));
			Assert.AreEqual(17, HexMap.GetIndex(2, 10));
			Assert.AreEqual(18, HexMap.GetIndex(2, 11));
			Assert.AreEqual(7, HexMap.GetIndex(2, 12));

			Assert.AreEqual(19, HexMap.GetIndex(3, 0));

			// radius = -1
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetIndex(-1); });
			Assert.AreEqual(0, HexMap.GetIndex(0));
			Assert.AreEqual(1, HexMap.GetIndex(1));
			Assert.AreEqual(7, HexMap.GetIndex(2));
			Assert.AreEqual(19, HexMap.GetIndex(3));
		}


	}
}