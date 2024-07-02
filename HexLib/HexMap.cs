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
		public static int GetMapSize(int Radius)
		{
			if (Radius < 0) throw (new ArgumentException("Radius"));
			return Radius * (Radius + 1) / 2 * 6 + 1;
		}


		public int GetDistance(HexCoordinate CoordinateA, HexCoordinate CoordinateB)
		{
			
			return -1;
		}

	}



	public class HexMap<T> : HexMap,IHexMap<T>
	{
		private T[] items;
		public T this[HexCoordinate Coordinate]
		{
			get
			{
				return items[0];
			}
			set
			{
				items[0] = value;
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
			/*if (Radius < 0) throw (new ArgumentException("Radius"));
			this.Radius = Radius;
			Count = HexMap.GetMapSize(Radius);
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

			if (Radius>0) for (int r=0;r<=Radius;r++)
			{
				for (int i = 0; i < GetPerimeter(r); i++)
				{
					FillDistanceToOld(new HexCoordinate(r, i), new HexCoordinate(r, i), 0);
				}
				
			}*/

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
		
		/*public IEnumerable<HexCoordinate> GetNeighbours(HexCoordinate Coordinate)
		{
			return Coordinate.GetNeighbours().Where(item => item.Index < Count);
		}*/
		/*public IEnumerable<HexCoordinate> DrawLine(HexCoordinate A, HexCoordinate B)
		{
			bool useMaxIndex=false;
			yield return A;
			
			while(!A.Equals(B))
			{
				A = GetNearestCoordinateFromAToBOld(A, B,useMaxIndex);
				if (A.Index > Count) yield break;
				yield return A;
				useMaxIndex = !useMaxIndex;
			}
		}*/

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
