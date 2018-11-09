﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexLib;

namespace HexLibTest
{
	[TestClass]
	public class HexCoordinateUnitTest
	{

		[TestMethod]
		public void ShouldNotCreateCoordinateWithNegativeRadius()
		{
			Assert.ThrowsException<ArgumentException>(() => { new HexCoordinate(-1, 0); });
			Assert.ThrowsException<ArgumentException>(() => { new HexCoordinate(-3, 0); });

		}

		[TestMethod]
		public void ShouldModuloRingIndex()
		{
			// radius = 0
			for (int t = -10; t < 10; t++) Assert.AreEqual(0,new HexCoordinate(0,t).RingIndex);

			// radius = 1
			for (int t = 0; t < 6; t++) Assert.AreEqual(t, new HexCoordinate(1, t).RingIndex);
			for (int t = 6; t < 12; t++) Assert.AreEqual(t - 6, new HexCoordinate(1, t).RingIndex);
			Assert.AreEqual(5, new HexCoordinate(1, -1).RingIndex);
			Assert.AreEqual(4, new HexCoordinate(1, -2).RingIndex);
			Assert.AreEqual(3, new HexCoordinate(1, -3).RingIndex);
			Assert.AreEqual(2, new HexCoordinate(1, -4).RingIndex);
			Assert.AreEqual(1, new HexCoordinate(1, -5).RingIndex);

			// radius = 2
			for (int t = 0; t < 12; t++) Assert.AreEqual(t, new HexCoordinate(2, t).RingIndex);
			for (int t = 12; t < 24; t++) Assert.AreEqual(t - 12, new HexCoordinate(2, t).RingIndex);
			Assert.AreEqual(11, new HexCoordinate(2, -1).RingIndex);
			Assert.AreEqual(10, new HexCoordinate(2, -2).RingIndex);
			Assert.AreEqual(9, new HexCoordinate(2, -3).RingIndex);
			Assert.AreEqual(8, new HexCoordinate(2, -4).RingIndex);
			Assert.AreEqual(7, new HexCoordinate(2, -5).RingIndex);
		}

		[TestMethod]
		public void ShouldHaveCorrectIndex()
		{
			// radius = 0
			for (int t = -10; t < 10; t++) Assert.AreEqual(0, new HexCoordinate(0, t).Index);

			Assert.AreEqual(1, new HexCoordinate(1, 0).Index);
			Assert.AreEqual(2, new HexCoordinate(1, 1).Index);
			Assert.AreEqual(3, new HexCoordinate(1, 2).Index);
			Assert.AreEqual(4, new HexCoordinate(1, 3).Index);
			Assert.AreEqual(5, new HexCoordinate(1, 4).Index);
			Assert.AreEqual(6, new HexCoordinate(1, 5).Index);
			Assert.AreEqual(1, new HexCoordinate(1, 6).Index);

			Assert.AreEqual(7, new HexCoordinate(2, 0).Index);
			Assert.AreEqual(8, new HexCoordinate(2, 1).Index);
			Assert.AreEqual(9, new HexCoordinate(2, 2).Index);
			Assert.AreEqual(10, new HexCoordinate(2, 3).Index);
			Assert.AreEqual(11, new HexCoordinate(2, 4).Index);
			Assert.AreEqual(12, new HexCoordinate(2, 5).Index);
			Assert.AreEqual(13, new HexCoordinate(2, 6).Index);
			Assert.AreEqual(14, new HexCoordinate(2, 7).Index);
			Assert.AreEqual(15, new HexCoordinate(2, 8).Index);
			Assert.AreEqual(16, new HexCoordinate(2, 9).Index);
			Assert.AreEqual(17, new HexCoordinate(2, 10).Index);
			Assert.AreEqual(18, new HexCoordinate(2, 11).Index);
			Assert.AreEqual(7, new HexCoordinate(2, 12).Index);

			Assert.AreEqual(19, new HexCoordinate(3, 0).Index);


		}


		[TestMethod]
		public void ShouldHaveCorrectDirectionIndex()
		{
			HexCoordinate coord;

			for (int t = 0; t < 6; t++)
			{
				coord = new HexCoordinate(1, t);
				Assert.AreEqual(t, coord.DirectionIndex);
			}

			coord = new HexCoordinate(0, 5); Assert.AreEqual(0, coord.DirectionIndex);

			coord = new HexCoordinate(2, 0); Assert.AreEqual(0, coord.DirectionIndex);
			coord = new HexCoordinate(2, 1); Assert.AreEqual(0, coord.DirectionIndex);
			coord = new HexCoordinate(2, 2); Assert.AreEqual(1, coord.DirectionIndex);
			coord = new HexCoordinate(2, 3); Assert.AreEqual(1, coord.DirectionIndex);
			coord = new HexCoordinate(2, 4); Assert.AreEqual(2, coord.DirectionIndex);
			coord = new HexCoordinate(2, 5); Assert.AreEqual(2, coord.DirectionIndex);
			coord = new HexCoordinate(2, 6); Assert.AreEqual(3, coord.DirectionIndex);
			coord = new HexCoordinate(2, 7); Assert.AreEqual(3, coord.DirectionIndex);
			coord = new HexCoordinate(2, 8); Assert.AreEqual(4, coord.DirectionIndex);
			coord = new HexCoordinate(2, 9); Assert.AreEqual(4, coord.DirectionIndex);
			coord = new HexCoordinate(2, 10); Assert.AreEqual(5, coord.DirectionIndex);
			coord = new HexCoordinate(2, 11); Assert.AreEqual(5, coord.DirectionIndex);

			coord = new HexCoordinate(2, -1); Assert.AreEqual(5, coord.DirectionIndex);
			coord = new HexCoordinate(2, -2); Assert.AreEqual(5, coord.DirectionIndex);
			coord = new HexCoordinate(2, -3); Assert.AreEqual(4, coord.DirectionIndex);
			coord = new HexCoordinate(2, -4); Assert.AreEqual(4, coord.DirectionIndex);

		}


		[TestMethod]
		public void ShouldRotateTransform()
		{
			HexCoordinate coord;

			coord = new HexCoordinate(2, 0).RotateTransform(1); Assert.AreEqual(1, coord.RingIndex);
			coord = new HexCoordinate(2, 0).RotateTransform(2); Assert.AreEqual(2, coord.RingIndex);
			coord = new HexCoordinate(2, 0).RotateTransform(3); Assert.AreEqual(3, coord.RingIndex);
			coord = new HexCoordinate(2, 0).RotateTransform(12); Assert.AreEqual(0, coord.RingIndex);
			coord = new HexCoordinate(2, 0).RotateTransform(-1); Assert.AreEqual(11, coord.RingIndex);
			coord = new HexCoordinate(2, 11).RotateTransform(1); Assert.AreEqual(0, coord.RingIndex);

		}

		[TestMethod]
		public void ShouldJumpTransform()
		{
			HexCoordinate coord;


			coord = new HexCoordinate(2, 0).JumpTransform(1); Assert.AreEqual(3, coord.Radius); Assert.AreEqual(0, coord.RingIndex);
			coord = new HexCoordinate(2, 0).JumpTransform(2); Assert.AreEqual(4, coord.Radius); Assert.AreEqual(0, coord.RingIndex);
			coord = new HexCoordinate(2, 0).JumpTransform(-1); Assert.AreEqual(1, coord.Radius); Assert.AreEqual(0, coord.RingIndex);
			coord = new HexCoordinate(2, 0).JumpTransform(-2); Assert.AreEqual(0, coord.Radius); Assert.AreEqual(0, coord.RingIndex);

			coord = new HexCoordinate(2, 1).JumpTransform(1); Assert.AreEqual(3, coord.Radius); Assert.AreEqual(1, coord.RingIndex);
			coord = new HexCoordinate(2, 1).JumpTransform(2); Assert.AreEqual(4, coord.Radius); Assert.AreEqual(2, coord.RingIndex);


		}

	}
}
