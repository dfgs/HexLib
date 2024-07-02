using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HexLib
{
	public interface IScreenCoordinate:IEquatable<IScreenCoordinate>
	{
		float X
		{
			get;
		}
		float Y
		{
			get;
		}


	}
}
