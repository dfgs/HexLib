using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexLib
{
	public struct ScreenCoordinate:IScreenCoordinate
	{
		public float X
		{
			get;
			set;
		}
		public float Y
		{
			get;
			set;
		}

		public ScreenCoordinate(float X,float Y)
		{
			this.X = X;this.Y = Y;
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public bool Equals(IScreenCoordinate Other)
		{
			return ((this.X == Other.X) && (this.Y == Other.Y));
		}

		public override string ToString()
		{
			return $"({X},{Y})";
		}

	}
}
