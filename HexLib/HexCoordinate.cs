using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexLib
{
	public struct HexCoordinate:IEquatable<HexCoordinate>
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

		public bool IsAxisAligned
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
				IsAxisAligned = true;
			}
			else
			{
				this.RingIndex=  RingIndex % HexMap.GetPerimeter(Radius);
				if (this.RingIndex < 0) this.RingIndex = this.RingIndex + HexMap.GetPerimeter(Radius);
				this.DirectionIndex=this.RingIndex / Radius;
				this.Index = Radius * (Radius - 1) / 2 * 6 + 1 + this.RingIndex;
				this.IsAxisAligned = (this.RingIndex % this.Radius) == 0;
			}


		}

		public override bool Equals(object obj)
		{
			if (obj is HexCoordinate) return Equals((HexCoordinate)obj);
			else return false;
		}

		public override int GetHashCode()
		{
			return this.Index;
		}

		public bool Equals(HexCoordinate Other)
		{
			return (Other.Index== this.Index);
		}

		public HexCoordinate RotateTransform(int Count)
		{
			return new HexCoordinate(Radius, RingIndex+Count);
		}

		public HexCoordinate JumpTransform(int Count)
		{
			return new HexCoordinate(Radius+Count, RingIndex + Count* DirectionIndex);
		}


		public IEnumerable<HexCoordinate> GetNeighbours()
		{
			if (this.Radius == 0)
			{
				yield return new HexCoordinate(1, 0);
				yield return new HexCoordinate(1, 1);
				yield return new HexCoordinate(1, 2);
				yield return new HexCoordinate(1, 3);
				yield return new HexCoordinate(1, 4);
				yield return new HexCoordinate(1, 5);
				yield break;
			}

			yield return this.RotateTransform(-1);
			yield return this.RotateTransform(1);

			if (this.IsAxisAligned)
			{
				yield return this.JumpTransform(-1);
				yield return this.JumpTransform(1);
				yield return this.JumpTransform(1).RotateTransform(-1);
				yield return this.JumpTransform(1).RotateTransform(1);
			}
			else
			{
				yield return this.JumpTransform(1);
				yield return this.JumpTransform(1).RotateTransform(1);
				yield return this.JumpTransform(-1);
				yield return this.JumpTransform(-1).RotateTransform(-1);

			}
		}

		
	}
}
