using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HexLib
{
	public class HexMap
	{
		protected HexMap()
		{
			
		}

		
		public static int GetPerimeter(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			if (Radius == 0) return 1;
			return 6 * Radius;
		}
		public static int GetCount(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			return Radius * (Radius + 1) / 2 * 6 + 1;
		}
		


		

		#region drawing coordinate transformation
		/*public static Point GetPosition(HexCoordinate Coordinate, double HexRadius)
		{
			double x, y;
			double angle;
			int mod;
			int directionIndex;
			double horDist;
			double vertDist;


			if (Coordinate.Radius == 0) return new Point(0, 0);

			vertDist = HexRadius * 3.0f / 4.0f;
			horDist = HexRadius * Math.Sqrt(3);


			mod = RingIndex % Radius; // was modulo ring index
			directionIndex = GetDirectionIndex(Radius, RingIndex);

			angle = 2 * Math.PI * directionIndex / 6.0f;

			x = 0; y = 0;
			switch (directionIndex)
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

		public static Point GetHexCorner(Point Position, double HexRadius, int Corner, double Margin = 0)
		{
			double angle_deg = 60 * Corner + 30;
			double angle_rad = Math.PI * angle_deg / 180;

			return new Point(Position.X + (HexRadius - Margin) * Math.Cos(angle_rad), Position.Y + (HexRadius - Margin) * Math.Sin(angle_rad));
		}

		public static PointCollection GetHexCorners(Point Position, double HexRadius, double Margin = 0)
		{
			double angle_deg;
			double angle_rad;
			PointCollection points = new PointCollection();

			for (int corner = 0; corner < 6; corner++)
			{
				angle_deg = 60 * corner + 30;
				angle_rad = Math.PI * angle_deg / 180;

				points.Add(new Point(Position.X + (HexRadius - Margin) * Math.Cos(angle_rad), Position.Y + (HexRadius - Margin) * Math.Sin(angle_rad)));
			}
			return points;
		}*/

		#endregion







	}
	public class HexMap<T> : HexMap,IHexMap<T>
	{
		private T[] items;
		public T this[HexCoordinate Coordinate]
		{
			get
			{
				return items[Coordinate.Index];
			}
			set
			{
				items[Coordinate.Index] = value;
			}
		}
		public T this[int Index]
		{
			get
			{
				return items[Index];
			}
			set
			{
				items[Index] = value;
			}
		}

		public int Radius
		{
			get;
			private set;
		}

		public int Count
		{
			get;
			private set;
		}

		public HexMap(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			this.Radius = Radius;
			Count = HexMap.GetCount(Radius);
			items = new T[Count];
		}

		public IEnumerator<T> GetEnumerator()
		{
			foreach (T item in items) yield return item;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (T item in items) yield return item;
		}

	
		
	}

}
