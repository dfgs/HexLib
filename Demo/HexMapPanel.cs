using HexLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Demo
{
	public class HexMapPanel : Panel
	{
		public static readonly DependencyProperty CoordinateProperty = DependencyProperty.RegisterAttached("Coordinate", typeof(HexCoordinate), typeof(HexMapPanel), new FrameworkPropertyMetadata(default(HexCoordinate), FrameworkPropertyMetadataOptions.AffectsParentArrange));


		public static readonly DependencyProperty HexRadiusProperty = DependencyProperty.Register("HexRadius", typeof(double), typeof(HexMapPanel), new FrameworkPropertyMetadata(16.0d,FrameworkPropertyMetadataOptions.AffectsMeasure|FrameworkPropertyMetadataOptions.AffectsArrange));
		public double HexRadius
		{
			get { return (double)GetValue(HexRadiusProperty); }
			set { SetValue(HexRadiusProperty, value); }
		}

		public HexMapPanel()
		{
		}

		public static HexCoordinate GetCoordinate(DependencyObject Component)
		{
			return (HexCoordinate)Component.GetValue(CoordinateProperty);
		}
		public static void SetCoordinate(DependencyObject Component,HexCoordinate Value)
		{
			Component.SetValue(CoordinateProperty, Value);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			HexCoordinate coordinate;
			Size itemSize;
			int maxRadius;

			maxRadius = 0;
			itemSize = new Size(HexRadius*2, HexRadius*2);
			foreach (UIElement element in Children)
			{
				coordinate = GetCoordinate(element);
				if (coordinate.Radius > maxRadius) maxRadius = coordinate.Radius;
				element.Measure(itemSize);
			}

			
			return new Size(2*(maxRadius+1)* HexRadius* Math.Sqrt(3), (2*maxRadius)*2*HexRadius) ;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			HexCoordinate coordinate;
			Size itemSize;
			Rect itemRect;
			Point position;
			double dx, dy;

			itemSize = new Size(HexRadius*2, HexRadius*2);
			dx = DesiredSize.Width / 2.0d-HexRadius;
			dy = DesiredSize.Height / 2.0d-HexRadius;

			foreach (UIElement element in Children)
			{
				coordinate = GetCoordinate(element);
				position = coordinate.ToScreenCoordinate(HexRadius);
				position.Offset(dx, dy);
				itemRect = new Rect(position, itemSize);
				element.Arrange(itemRect);
			}
			return DesiredSize;
		}


	}
}
