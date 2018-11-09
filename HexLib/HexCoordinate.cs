using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexLib
{
	public struct HexCoordinate
	{
		public int Radius
		{
			get;
			private set;
		}

		public int RingIndex
		{
			get;
			private set;
		}

		public int DirectionIndex
		{
			get;
			set;
		}

		public int Index
		{
			get;
			private set;
		}

		public HexCoordinate(int Radius,int RingIndex)
		{
			if (Radius < 0) throw new ArgumentException("Radius");
			this.Radius = Radius;

			if (Radius == 0)
			{
				this.RingIndex = 0;
				this.DirectionIndex = 0;
				this.Index = 0;
			}
			else
			{
				this.RingIndex= RingIndex<0? HexMap.GetPerimeter(Radius) + RingIndex % HexMap.GetPerimeter(Radius): RingIndex % HexMap.GetPerimeter(Radius);
				this.DirectionIndex=this.RingIndex / Radius;
				this.Index = Radius * (Radius - 1) / 2 * 6 + 1 + this.RingIndex;
			}


		}


		public HexCoordinate RotateTransform(int Count)
		{
			return new HexCoordinate(Radius, RingIndex+Count);
		}

		public HexCoordinate JumpTransform(int Count)
		{
			return new HexCoordinate(Radius+Count, RingIndex + Count* DirectionIndex);
		}


	}
}
