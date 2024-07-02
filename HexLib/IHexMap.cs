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

	}

	public interface IHexMap<T>: IHexMap,IEnumerable<T>
	{
		T this[HexCoordinate Coordinate]
		{
			get;
			set;
		}
		T this[int Index]
		{
			get;
			set;
		}


	}
}
