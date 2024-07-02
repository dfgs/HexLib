using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexLib;
using System.Linq;

namespace HexLibTest
{
	[TestClass]
	public class ScreenCoordinateUnitTest
	{

		[TestMethod]
		public void ShouldCreateCoordinate()
		{
			ScreenCoordinate c;

			c = new ScreenCoordinate();
			Assert.AreEqual(0, c.X);
			Assert.AreEqual(0, c.Y);

			c = new ScreenCoordinate(1,2);
			Assert.AreEqual(1, c.X);
			Assert.AreEqual(2, c.Y);


		}

		[TestMethod]
		public void ShouldCompareEquality()
		{
			Assert.IsTrue(new ScreenCoordinate(0, 0).Equals(new ScreenCoordinate(0, 0)));
			Assert.IsTrue(new ScreenCoordinate(1, 0).Equals(new ScreenCoordinate(1, 0)));
			Assert.IsTrue(new ScreenCoordinate(1, 5).Equals(new ScreenCoordinate(1, 5)));
			Assert.IsFalse(new ScreenCoordinate(1, 0).Equals(new ScreenCoordinate(2, 0)));
			Assert.IsFalse(new ScreenCoordinate(0, 1).Equals(new ScreenCoordinate(0, 2)));
		}

		[TestMethod]
		public void ShouldCompareHashCode()
		{
			Assert.AreEqual(new ScreenCoordinate(0, 0).GetHashCode(), new ScreenCoordinate(0, 0).GetHashCode());
			Assert.AreEqual(new ScreenCoordinate(1, 0).GetHashCode(), new ScreenCoordinate(1, 0).GetHashCode());
			Assert.AreEqual(new ScreenCoordinate(0, 1).GetHashCode(), new ScreenCoordinate(0, 1).GetHashCode());

			Assert.AreNotEqual(new ScreenCoordinate(0, 0).GetHashCode(), new ScreenCoordinate(1, 1).GetHashCode());
			Assert.AreNotEqual(new ScreenCoordinate(1, 0).GetHashCode(), new ScreenCoordinate(0, 1).GetHashCode());
			Assert.AreNotEqual(new ScreenCoordinate(0, -10).GetHashCode(), new ScreenCoordinate(0, -9).GetHashCode());
		}


		





	}
}
