using HexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo
{
	public class HexMapPanel : Panel
	{
		public static readonly DependencyProperty CoordinateProperty = DependencyProperty.RegisterAttached("Coordinate", typeof(IHexCoordinate), typeof(HexMapPanel), new FrameworkPropertyMetadata(default(HexCoordinate), FrameworkPropertyMetadataOptions.AffectsParentArrange));


		public static readonly DependencyProperty HexRadiusProperty = DependencyProperty.Register("HexRadius", typeof(double), typeof(HexMapPanel), new FrameworkPropertyMetadata(16.0d,FrameworkPropertyMetadataOptions.AffectsMeasure|FrameworkPropertyMetadataOptions.AffectsArrange));
		public double HexRadius
		{
			get { return (double)GetValue(HexRadiusProperty); }
			set { SetValue(HexRadiusProperty, value); }
		}

		public HexMapPanel()
		{
		}

		public static IHexCoordinate GetCoordinate(DependencyObject Component)
		{
			return (IHexCoordinate)Component.GetValue(CoordinateProperty);
		}
		public static void SetCoordinate(DependencyObject Component,IHexCoordinate Value)
		{
			Component.SetValue(CoordinateProperty, Value);
		}

		public static PointCollection GetHexCorners(double HexRadius, double Margin = 0)
		{
			double angle_deg;
			double angle_rad;
			PointCollection points = new PointCollection();

			for (int corner = 0; corner < 6; corner++)
			{
				angle_deg = 60 * corner + 30;
				angle_rad = Math.PI * angle_deg / 180;

				points.Add(new Point(HexRadius * Math.Sqrt(3) / 2f + (HexRadius - Margin) * Math.Cos(angle_rad), HexRadius + (HexRadius - Margin) * (float)Math.Sin(angle_rad)));
			}
			return points;
		}

		public static Point ToPoint(IHexCoordinate Coordinate, double HexRadius)
		{
			double x, y;
			double vertDist, horDist;

			vertDist = 2*HexRadius * 3.0f / 4.0f;
			horDist = HexRadius * Math.Sqrt(3);


			if (HexRadius == 0) return new Point(0, 0);
			if ((Coordinate.A & 1) != 0)
			{
				x = Coordinate.A * horDist /3.0f;
				y = Coordinate.B * 2 * HexRadius;
			}
			else
			{
				if ((Coordinate.B & 1) != 0)
				{
					x = Coordinate.A * horDist / 3.0f;
					y = Coordinate.B * 2 * HexRadius-5;
				}
				else
				{
					x = Coordinate.A * horDist / 3.0f;
					y = Coordinate.B * 2 * HexRadius+5;

				}
			}
			return new Point(x+100,y+100);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			IHexCoordinate coordinate;
			Size itemSize;
			int maxA, maxB;
			double vertDist, horDist;

			vertDist = 2 * HexRadius * 3.0f / 4.0f;
			horDist = HexRadius * Math.Sqrt(3);

			maxA = 0;maxB = 0;

			itemSize = new Size(horDist, vertDist) ;
			foreach (UIElement element in Children)
			{
				coordinate = GetCoordinate(element);
				if (coordinate.A > maxA) maxA = coordinate.A;
				if (coordinate.B > maxB) maxB = coordinate.B;
				element.Measure(itemSize);
			}


			return  new Size((maxA+1.5)* horDist, maxB*vertDist+HexRadius*2) ;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			IHexCoordinate coordinate;
			Size itemSize;
			Rect itemRect;
			Point position;

			itemSize = new Size(HexRadius * Math.Sqrt(3), HexRadius * 2) ;

			foreach (UIElement element in Children)
			{
				coordinate = GetCoordinate(element);
				position = ToPoint(coordinate, HexRadius);
				position.Offset(-itemSize.Width / 2, -itemSize.Height / 2);
				itemRect = new Rect(position, itemSize);
				element.Arrange(itemRect);
			}
			return finalSize;
		}


	}
}
