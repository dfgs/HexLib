using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexLib
{
	public interface IHexMap
	{
		uint Width
		{
			get;
		}
		uint Height
		{
			get;
		}
		uint Count
		{
			get;
		}

		uint GetIndex(IHexCoordinate Coordinate);
		bool IsOutOfBound(IHexCoordinate Coordinate);

	}

	public interface IHexMap<T>: IHexMap,IEnumerable<T>
	{
		T this[IHexCoordinate Coordinate]
		{
			get;
			set;
		}
		T this[uint Index]
		{
			get;
			set;
		}


	}
}
