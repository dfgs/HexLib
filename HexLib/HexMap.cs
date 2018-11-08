using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static int GetModuloRingIndex(int Radius, int RingIndex)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			if (Radius == 0) return 0;
			if (RingIndex < 0) return GetPerimeter(Radius) + RingIndex % GetPerimeter(Radius);
			return RingIndex % GetPerimeter(Radius);
		}

		public static int GetIndex(int Radius, int RingIndex)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			return GetIndex(Radius) + GetModuloRingIndex(Radius, RingIndex);
		}

		public static int GetIndex(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			if (Radius == 0) return 0;
			return Radius * (Radius - 1) / 2 * 6 + 1;
		}

		/*
		 * 
		 * 
		protected static int GetItemOffset(int Radius, int Index)
		{
			return GetItemOffset(Radius) + GetModIndex(Radius, Index);
		}

		public static int GetModIndex(int Radius, int Index)
		{
			if (Radius < 0) throw (new ArgumentException("Radius must be greater or equal to zero"));
			if (Radius == 0) return 0;
			if (Index < 0) return GetPerimeter(Radius) + Index % GetPerimeter(Radius);
			return Index % GetPerimeter(Radius);
		}

		public static Point GetPosition(double HexRadius, int Radius, int Index)
		{
			double x, y;
			double angle;
			int mod;
			int branch;
			double horDist;
			double vertDist;

			int index;

			if (Radius == 0) return new Point(0, 0);

			vertDist = HexRadius * 3.0f / 4.0f;
			horDist = HexRadius * Math.Sqrt(3);

			index = GetModIndex(Radius, Index);

			mod = index % Radius;
			branch = index / Radius;

			angle = 2 * Math.PI * branch / 6.0f;

			x = 0; y = 0;
			switch (branch)
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


		public static Point GetCorner(Point Position, double HexRadius, int Corner, double Margin = 0)
		{
			double angle_deg = 60 * Corner + 30;
			double angle_rad = Math.PI * angle_deg / 180;

			return new Point(Position.X + (HexRadius - Margin) * Math.Cos(angle_rad), Position.Y + (HexRadius - Margin) * Math.Sin(angle_rad));
		}

		public static PointCollection GetCorners(Point Position, double HexRadius, double Margin = 0)
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
		}

		public IEnumerable<NeighborCoordinate> GetNeighborCoordinates(int Radius, int Index)
		{
			int index;
			int mod;
			int currentBranch;
			//int previousBranch;


			if (Radius == 0)
			{
				if (this.Radius > 0)
				{
					yield return new NeighborCoordinate(1, 0, 0, 3);
					yield return new NeighborCoordinate(1, 1, 1, 3);
					yield return new NeighborCoordinate(1, 2, 2, 3);
					yield return new NeighborCoordinate(1, 3, 3, 3);
					yield return new NeighborCoordinate(1, 4, 4, 3);
					yield return new NeighborCoordinate(1, 5, 5, 3);
				}
				yield break;
			}

			index = GetModIndex(Radius, Index);

			mod = index % Radius;
			currentBranch = index / Radius;


			yield return new NeighborCoordinate(Radius, Index + 1, 2 + currentBranch, 3); //2+branch
			yield return new NeighborCoordinate(Radius, Index - 1, mod == 0 ? 4 + currentBranch : 5 + currentBranch, 3);

			yield return new NeighborCoordinate(Radius - 1, Index - currentBranch, 3 + currentBranch, 3);//3+branch
			if (mod != 0) yield return new NeighborCoordinate(Radius - 1, Index - currentBranch - 1, 4 + currentBranch, 3);


			if (Radius < this.Radius)
			{
				yield return new NeighborCoordinate(Radius + 1, Index + currentBranch, 0 + currentBranch, 3);//0+branch
				yield return new NeighborCoordinate(Radius + 1, Index + currentBranch + 1, 1 + currentBranch, 3); //1+branch
				if (mod == 0) yield return new NeighborCoordinate(Radius + 1, Index + currentBranch - 1, 5 + currentBranch, 3); // 5+branch
			}

			yield break;
		}
		//*/

	}
	public class HexMap<T> : HexMap,IHexMap<T>
	{
		private T[] items;
		public T this[int Radius, int RingIndex]
		{
			get
			{
				return items[GetIndex(Radius, RingIndex)];
			}
			set
			{
				items[GetIndex(Radius, RingIndex)] = value;
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
			Count = Radius * (Radius + 1) / 2 * 6 + 1;
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

		/*public IEnumerable<T> GetNeighbors(int Radius, int Index)
		{
			foreach (NeighborCoordinate coord in GetNeighborCoordinates(Radius, Index)) yield return this[coord.Radius, coord.Index];
		}*/

		
	}

}
