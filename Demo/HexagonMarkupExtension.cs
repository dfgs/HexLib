using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Demo
{
    public class HexagonMarkupExtension:MarkupExtension
    {

		public double Radius { get; set; }
		public double Margin { get; set; }
		public bool Horizontal { get; set; }

		public HexagonMarkupExtension() 
		{
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			double angle_deg;
			double angle_rad;
			PointCollection points = new PointCollection();

			for (int corner = 0; corner < 6; corner++)
			{
				if (Horizontal) angle_deg = 60 * corner ;
				else angle_deg = 60 * corner + 30;
				angle_rad = Math.PI * angle_deg / 180;

				points.Add(new Point(Radius * Math.Sqrt(3) / 2f + (Radius - Margin) * Math.Cos(angle_rad), Radius + (Radius - Margin) * (float)Math.Sin(angle_rad)));
			}
			return points;
		}

	}
}
