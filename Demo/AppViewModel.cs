using HexLib;
using System;
using System.Linq;
using System.Windows;

namespace Demo
{
	public class AppViewModel:DependencyObject
	{
		public static DemoModes[] DemoModes = Enum.GetValues(typeof(DemoModes)).Cast<DemoModes>().ToArray();


		public static readonly double HexRadius=32;


		public static readonly DependencyProperty DemoModeProperty = DependencyProperty.Register("DemoMode", typeof(DemoModes), typeof(AppViewModel),new PropertyMetadata(Demo.DemoModes.Coordinates,DemoModePropertyChanged));
		public DemoModes DemoMode
		{
			get { return (DemoModes)GetValue(DemoModeProperty); }
			set { SetValue(DemoModeProperty, value); }
		}


		public static readonly DependencyProperty HexMapProperty = DependencyProperty.Register("HexMap", typeof(HexMap<HexViewModel>), typeof(AppViewModel));
		public HexMap<HexViewModel> HexMap
		{
			get { return (HexMap<HexViewModel>)GetValue(HexMapProperty); }
			set { SetValue(HexMapProperty, value); }
		}


		public AppViewModel()
		{
			HexViewModel item;

			HexMap = new HexMap<HexViewModel>(5);
			for(int r=0;r<=5;r++)
			{
				for(int i=0;i<HexLib.HexMap.GetPerimeter(r);i++)
				{
					item = new HexViewModel(new HexCoordinate(r, i),HexRadius) ;
					HexMap[item.Coordinate] = item;
				}
			}
			OnDemoModeChanged();

		}


		private static void DemoModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((AppViewModel)d).OnDemoModeChanged();
		}
		protected void OnDemoModeChanged()
		{
			if (HexMap == null) return;

			switch (DemoMode)
			{
				case Demo.DemoModes.Coordinates:
					OnUpdateHexContentWithCoordinates();
					break;
				case Demo.DemoModes.Neighbours:
					OnClearHexContent();
					break;
				default:
					OnClearHexContent();
					break;
			}

			
		}
		protected void OnUpdateHexContentWithCoordinates()
		{
			foreach (HexViewModel hex in HexMap)
			{
				hex.Content = hex.Coordinate;
			}
		}
		protected void OnClearHexContent()
		{
			foreach (HexViewModel hex in HexMap)
			{
				hex.Content = null;
			}
		}


	}
}
