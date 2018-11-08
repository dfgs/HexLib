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
		int Radius
		{
			get;
		}

		int Count
		{
			get;
		}

	}

	public interface IHexMap<T>:IEnumerable<T>,IHexMap
	{
		T this[int Radius, int RingIndex]
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
