﻿using System;
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

		protected int[,] distances;

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

		public int GetDistance(HexCoordinate A,HexCoordinate B)
		{
			return distances[A.Index, B.Index];
		}
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

			distances = new int[Count, Count];
			for(int x=0;x<Count;x++)
			{
				for (int y = 0; y < Count; y++)
				{
					distances[x, y] = int.MaxValue;
				}
			}

			for (int r = 0; r <= Radius; r++)
			{
				for (int i = 0; i < GetPerimeter(r); i++)
				{
					HexCoordinate coordinate = new HexCoordinate(r, i);
					distances[coordinate.Index, coordinate.Index] = 0;
					distances[0, coordinate.Index] = r;
					distances[coordinate.Index,0] = r;
				}
			}

			for (int r=0;r<=Radius;r++)
			{
				for (int i = 0; i < GetPerimeter(r); i++)
				{
					FillDistanceTo(new HexCoordinate(r, i), new HexCoordinate(r, i), 0);
				}
				
			}

			/*string s = "";
			for (int x = 0; x < Count; x++)
			{
				for (int y = 0; y < Count; y++)
				{
					int d = distances[y, x];
					s += d;
					if (d < 10) s +=  "  "; else s += " ";
				}
				s += "\r\n";
			}

			s = "";*/
		}
		
		public IEnumerable<HexCoordinate> GetNeighbours(HexCoordinate Coordinate)
		{
			return Coordinate.GetNeighbours().Where(item => item.Index < Count);
		}


		private void FillDistanceTo(HexCoordinate Origin, HexCoordinate Point, int Dist)
		{
			int minDist;

			minDist = GetNeighbours(Point).Min(neighbourCoordinate => distances[Origin.Index, neighbourCoordinate.Index])+1;
			if (minDist < Dist) Dist = minDist;

			distances[Origin.Index, Point.Index] = Dist;
			distances[Point.Index, Origin.Index] = Dist;

			int newDist = Dist + 1;
			foreach (HexCoordinate neighbourCoordinate in GetNeighbours(Point))
			{
				if (distances[Origin.Index, neighbourCoordinate.Index] > newDist)
				{
					FillDistanceTo(Origin, neighbourCoordinate, newDist);
				}
			}
			
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
