using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HexLib
{
	public struct HexCoordinate:IHexCoordinate
	{
		public int A
		{
			get;
			set;
		}
		public int B
		{
			get;
			set;
		}
		public int Type
		{
			get => Math.Abs(A + B) % 2;
		}

		public HexCoordinate(int A,int B)
		{
			this.A = A;this.B = B;
		}

		public override bool Equals(object obj)
		{
			if (obj is HexCoordinate) return Equals((HexCoordinate)obj);
			else return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public bool Equals(IHexCoordinate Other)
		{
			return ((this.A == Other.A) && (this.B == Other.B));
		}

		public IEnumerable<IHexCoordinate> GetNeighbors()
		{
			if ((B&1)==0)
			{
				yield return new HexCoordinate(A - 1, B);
				yield return new HexCoordinate(A + 1, B);
				yield return new HexCoordinate(A , B - 1);
				yield return new HexCoordinate(A + 1, B - 1);
				yield return new HexCoordinate(A , B + 1);
				yield return new HexCoordinate(A + 1, B + 1);
			}
			else
			{
				yield return new HexCoordinate(A - 1, B);
				yield return new HexCoordinate(A + 1, B);
				yield return new HexCoordinate(A - 1, B - 1);
				yield return new HexCoordinate(A , B - 1);
				yield return new HexCoordinate(A - 1, B + 1);
				yield return new HexCoordinate(A , B + 1);

			}

		}

		public uint GetDistanceTo(IHexCoordinate Other)
		{

			var qA = this.A - (this.B + (this.B & 1)) / 2;
			var rA = this.B;
			var sA = -qA - rA;

			var qB = Other.A - (Other.B + (Other.B & 1)) / 2;
			var rB = Other.B;
			var sB = -qB - rB;

			return (uint)(Math.Abs(qA - qB) + Math.Abs(rA - rB) + Math.Abs(sA - sB)) / 2;
			

		}






		public override string ToString()
		{
			return $"({A},{B})";
		}

		
	}
}
