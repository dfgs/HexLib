using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

		public Point ToScreenCoordinate(double HexRadius)
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

		public  Point GetHexCorner(Point Center,double HexRadius, int Corner, double Margin = 0)
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
		}



	}
}
