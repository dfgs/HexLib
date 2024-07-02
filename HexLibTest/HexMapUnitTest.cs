using System;
using HexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HexLibTest
{
	[TestClass]
	public class HexMapUnitTest
	{
		[TestMethod]
		public void ShouldFailToCreateMapWithNullWidth()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => { new HexMap<string>(0, 10); });
		}
		[TestMethod]
		public void ShouldFailToCreateMapWithNullHeight()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => { new HexMap<string>(10, 0); });
		}

		[TestMethod]
		public void ShouldSuccessToCreateMapWithPositiveSize()
		{
			IHexMap map;

			map = new HexMap<string>(9,10);
			Assert.AreEqual(90u, map.Count);

	
		}

	
		/*[TestMethod]
		public void ShouldReturnCorrectMapSize()
		{
			Assert.ThrowsException<ArgumentException>(() => { HexMap.GetPerimeter(-1); });
			Assert.AreEqual(1, HexMap.GetMapSize(0));
			Assert.AreEqual(7, HexMap.GetMapSize(1));
			Assert.AreEqual(19, HexMap.GetMapSize(2));
			Assert.AreEqual(37, HexMap.GetMapSize(3));
		}*/

		[TestMethod]
		public void ShouldEnumerate()
		{
			HexMap<int> map;

			map = new HexMap<int>(9,10);
			Assert.AreEqual(90u, map.Count);
			Assert.AreEqual(map.Count, (uint)map.ToArray().Length);

		}

		[TestMethod]
		public void ShouldGetAndSetIndexer()
		{
			HexMap<HexCoordinate> map;
			HexCoordinate coordinate;

			map = new HexMap<HexCoordinate>(9,10);
			for(int A=0;A<9;A++)
			{
				for(int B=0;B<10;B++)
				{
					coordinate = new HexCoordinate(A, B);
					map[coordinate] = coordinate;
				}
			}

			for (int A = 0; A < 9; A++)
			{
				for (int B = 0; B < 10; B++)
				{
					coordinate = new HexCoordinate(A, B);
					Assert.AreEqual(coordinate, map[coordinate]);
				}
			}



		}



	}
}