using HexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Demo
{
	public class HexViewModel:DependencyObject
	{

		public static readonly DependencyProperty CoordinateProperty = DependencyProperty.Register("Coordinate", typeof(HexCoordinate), typeof(HexViewModel));
		public HexCoordinate Coordinate
		{
			get { return (HexCoordinate)GetValue(CoordinateProperty); }
			private set { SetValue(CoordinateProperty, value); }
		}


		public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(PointCollection), typeof(HexViewModel));
		public PointCollection Points
		{
			get { return (PointCollection)GetValue(PointsProperty); }
			set { SetValue(PointsProperty, value); }
		}


		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(HexViewModel));
		public object Content
		{
			get { return (object)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}


		public HexViewModel(HexCoordinate Coordinate,double HexRadius)
		{
			this.Coordinate = Coordinate;
			this.Points = Coordinate.GetHexCorners(new Point(HexRadius,HexRadius), HexRadius);
		}


	}
}
