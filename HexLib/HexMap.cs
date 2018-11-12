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
