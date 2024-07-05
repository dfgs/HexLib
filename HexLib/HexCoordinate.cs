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
			yield return new HexCoordinate(A - 1, B);
			yield return new HexCoordinate(A + 1, B);
			if (Type == 1) yield return new HexCoordinate(A, B - 1);
			else yield return new HexCoordinate(A, B + 1);
		}
		


	



		public override string ToString()
		{
			return $"({A},{B})";
		}

		
	}
}
