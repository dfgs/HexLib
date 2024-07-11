using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HexLib
{
	public interface IHexCoordinate:IEquatable<IHexCoordinate>
	{
		int A
		{
			get;
		}
		int B
		{
			get;
		}

		int Type
		{
			get;
		}

		IEnumerable<IHexCoordinate> GetNeighbors();

		uint GetDistanceTo(IHexCoordinate Other);

	}
}
