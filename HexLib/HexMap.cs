using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HexLib
{
	public class HexMap:IHexMap
	{
		public required uint Width
		{
			get;
			init;
		}
		public required uint Height
		{
			get;
			init;
		}

		public  uint Count
		{
			get;
			private set;
		}

		[SetsRequiredMembers]
		protected HexMap(uint Width,uint Height)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Width, nameof(Width));
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Height, nameof(Height));

			this.Width = Width;this.Height = Height;
			this.Count = Width * Height;
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
				return items[Coordinate.B*Width+Coordinate.A];
			}
			set
			{
				items[Coordinate.B * Width + Coordinate.A] = value;
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

		[SetsRequiredMembers]
		public HexMap(uint Width,uint Height):base(Width,Height)
		{
			items = new T[Count];

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
