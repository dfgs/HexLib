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
	public class EdgeViewModel:DependencyObject
	{


		public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(string), typeof(EdgeViewModel),new PropertyMetadata("Transparent"));
		public string Background
		{
			get { return (string)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}



		public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(EdgeViewModel), new PropertyMetadata(0d));
		public double X1
		{
			get { return (double)GetValue(X1Property); }
			set { SetValue(X1Property, value); }
		}

		public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(EdgeViewModel), new PropertyMetadata(0d));
		public double Y1
		{
			get { return (double)GetValue(Y1Property); }
			set { SetValue(Y1Property, value); }
		}

		public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(EdgeViewModel), new PropertyMetadata(0d));
		public double X2
		{
			get { return (double)GetValue(X2Property); }
			set { SetValue(X2Property, value); }
		}

		public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(EdgeViewModel), new PropertyMetadata(0d));
		public double Y2
		{
			get { return (double)GetValue(Y2Property); }
			set { SetValue(Y2Property, value); }
		}







		public EdgeViewModel(IHexCoordinate A, IHexCoordinate B, double HexRadius)
		{

			Point a, b;

			a = HexMapPanel.ToPoint(A, HexRadius);
			b = HexMapPanel.ToPoint(B, HexRadius);

			this.X1 = a.X; this.Y1 = a.Y;
			this.X2 = b.X; this.Y2 = b.Y;


		}


	}
}
