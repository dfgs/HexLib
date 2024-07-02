using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
			get => (A + B) % 2;
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


		/*public Point ToScreenCoordinate(double HexRadius)
		{
			double x, y;
			double angle;
			int mod;
			double horDist;
			double vertDist;


			if (Radius == 0) return new Point(0, 0);

			vertDist = HexRadius * 3.0f / 4.0f;
			horDist = HexRadius * Math.Sqrt(3);

			mod = RingIndex % Radius; // was modulo ring index
			angle = 2 * Math.PI * DirectionIndex / 6.0f;

			x = 0; y = 0;
			switch (DirectionIndex)
			{
				case 0:
					x = Radius * Math.Cos(angle) * horDist - 0.5f * mod * horDist;
					y = Radius * Math.Sin(angle) * horDist + 2 * mod * vertDist;
					break;
				case 1:
					x = Radius * Math.Cos(angle) * horDist - mod * horDist;
					y = Radius * Math.Sin(angle) * horDist;
					break;
				case 2:
					x = Radius * Math.Cos(angle) * horDist - 0.5f * mod * horDist;
					y = Radius * Math.Sin(angle) * horDist - 2 * mod * vertDist;
					break;
				case 3:
					x = Radius * Math.Cos(angle) * horDist + 0.5f * mod * horDist;
					y = Radius * Math.Sin(angle) * horDist - 2 * mod * vertDist;
					break;
				case 4:
					x = Radius * Math.Cos(angle) * horDist + mod * horDist;
					y = Radius * Math.Sin(angle) * horDist;
					break;
				case 5:
					x = Radius * Math.Cos(angle) * horDist + 0.5f * mod * horDist;
					y = Radius * Math.Sin(angle) * horDist + 2 * mod * vertDist;
					break;
			}

			return new Point(x, y);
		}
		//*/

		/*public Point GetHexCorner(Point Center,double HexRadius, int Corner, double Margin = 0)
		{
			double angle_deg = 60 * Corner + 30;
			double angle_rad = Math.PI * angle_deg / 180;

			return new Point(Center.X + (HexRadius - Margin) * Math.Cos(angle_rad), Center.Y + (HexRadius - Margin) * Math.Sin(angle_rad));
		}

		public  PointCollection GetHexCorners(Point Center, double HexRadius, double Margin = 0)
		{
			double angle_deg;
			double angle_rad;
			PointCollection points = new PointCollection();

			for (int corner = 0; corner < 6; corner++)
			{
				angle_deg = 60 * corner + 30;
				angle_rad = Math.PI * angle_deg / 180;

				points.Add(new Point(Center.X + (HexRadius - Margin) * Math.Cos(angle_rad), Center.Y + (HexRadius - Margin) * Math.Sin(angle_rad)));
			}
			return points;
		}*/

		public override string ToString()
		{
			return $"({A},{B})";
		}

		
	}
}
