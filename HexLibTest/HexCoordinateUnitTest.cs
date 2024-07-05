using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexLib;
using System.Linq;

namespace HexLibTest
{
	[TestClass]
	public class HexCoordinateUnitTest
	{

		[TestMethod]
		public void ShouldCreateCoordinate()
		{
			HexCoordinate c;

			c = new HexCoordinate();
			Assert.AreEqual(0, c.A);
			Assert.AreEqual(0, c.B);

			c = new HexCoordinate(1,2);
			Assert.AreEqual(1, c.A);
			Assert.AreEqual(2, c.B);


		}

		[TestMethod]
		public void ShouldCompareEquality()
		{
			Assert.IsTrue(new HexCoordinate(0, 0).Equals(new HexCoordinate(0, 0)));
			Assert.IsTrue(new HexCoordinate(1, 0).Equals(new HexCoordinate(1, 0)));
			Assert.IsTrue(new HexCoordinate(1, 5).Equals(new HexCoordinate(1, 5)));
			Assert.IsFalse(new HexCoordinate(1, 0).Equals(new HexCoordinate(2, 0)));
			Assert.IsFalse(new HexCoordinate(0, 1).Equals(new HexCoordinate(0, 2)));
		}

		[TestMethod]
		public void ShouldCompareHashCode()
		{
			Assert.AreEqual(new HexCoordinate(0, 0).GetHashCode(), new HexCoordinate(0, 0).GetHashCode());
			Assert.AreEqual(new HexCoordinate(1, 0).GetHashCode(), new HexCoordinate(1, 0).GetHashCode());
			Assert.AreEqual(new HexCoordinate(0, 1).GetHashCode(), new HexCoordinate(0, 1).GetHashCode());

			Assert.AreNotEqual(new HexCoordinate(0, 0).GetHashCode(), new HexCoordinate(1, 1).GetHashCode());
			Assert.AreNotEqual(new HexCoordinate(1, 0).GetHashCode(), new HexCoordinate(0, 1).GetHashCode());
			Assert.AreNotEqual(new HexCoordinate(0, -10).GetHashCode(), new HexCoordinate(0, -9).GetHashCode());
		}

		[TestMethod]
		public void ShouldReturnType()
		{
			HexCoordinate c;

			c = new HexCoordinate();
			Assert.AreEqual(0, c.Type);

			c = new HexCoordinate(1, 2);
			Assert.AreEqual(1, c.Type);

			c = new HexCoordinate(3, 3);
			Assert.AreEqual(0, c.Type);

			c = new HexCoordinate(3, 2);
			Assert.AreEqual(1, c.Type);

			c = new HexCoordinate(-1, 0);
			Assert.AreEqual(1, c.Type);
		}

		[TestMethod]
		public void ShouldReturnNeighbors()
		{
			HexCoordinate c;
			IHexCoordinate[] neighbors;

			c = new HexCoordinate();
			neighbors = c.GetNeighbors().ToArray();
			Assert.AreEqual(3, neighbors.Length);
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(-1, 0)));
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(1, 0)));
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(0, 1)));

			c = new HexCoordinate(1,1);
			neighbors = c.GetNeighbors().ToArray();
			Assert.AreEqual(3, neighbors.Length);
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(0, 1)));
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(2, 1)));
			Assert.IsTrue(neighbors.Contains(new HexCoordinate(1, 2)));


		}





	}
}
