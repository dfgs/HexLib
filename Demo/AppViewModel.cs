using HexLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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


		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(HexViewModel), typeof(AppViewModel), new PropertyMetadata(null, SelectedItemPropertyChanged));
		public HexViewModel SelectedItem
		{
			get { return (HexViewModel)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}



		public static readonly DependencyProperty PivotProperty = DependencyProperty.Register("Pivot", typeof(HexViewModel), typeof(AppViewModel), new PropertyMetadata(null, PivotPropertyChanged));
		public HexViewModel Pivot
		{
			get { return (HexViewModel)GetValue(PivotProperty); }
			set { SetValue(PivotProperty, value); }
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

		private static void PivotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((AppViewModel)d).OnPivotChanged((HexViewModel)e.OldValue,(HexViewModel)e.NewValue);
		}
		protected void OnPivotChanged(HexViewModel Old,HexViewModel New)
		{
			if (Old != null) Old.Background = "Transparent";
			if (New != null) New.Background = "GoldenRod";
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
				case Demo.DemoModes.Indices:
					OnUpdateHexContentWithIndices();
					break;
				case Demo.DemoModes.Neighbours:
					OnUpdateHexContentWithNeighbors();
					break;
				case Demo.DemoModes.Distance:
					OnUpdateHexContentWithDistance();
					break;
				default:
					OnClearHexContent();
					break;
			}
		}

		private static void SelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((AppViewModel)d).OnSelectedItemChanged();
		}
		protected void OnSelectedItemChanged()
		{
			if (HexMap == null) return;

			switch (DemoMode)
			{
				case Demo.DemoModes.Neighbours:
					OnUpdateHexContentWithNeighbors();
					break;
				case Demo.DemoModes.Distance:
					OnUpdateHexContentWithDistance();
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

		protected void OnUpdateHexContentWithIndices()
		{
			foreach (HexViewModel hex in HexMap)
			{
				hex.Content = hex.Coordinate.Index;
			}
		}

		protected void OnUpdateHexContentWithNeighbors()
		{
			foreach (HexViewModel hex in HexMap)
			{
				hex.Content = null;
			}
			if (SelectedItem == null) return;
			HexMap[SelectedItem.Coordinate].Content = SelectedItem.Coordinate;
			foreach(HexCoordinate coordinate in SelectedItem.Coordinate.GetNeighbours())
			{
				if (coordinate.Index >= HexMap.Count) continue;
				HexMap[coordinate].Content = coordinate;
			}
		}
		protected void OnUpdateHexContentWithDistance()
		{
			foreach (HexViewModel hex in HexMap)
			{
				if (hex == SelectedItem)
				{
					hex.Content = hex.Coordinate;
					continue;
				}
				if (SelectedItem == null) hex.Content = null;
				else hex.Content = hex.Coordinate.GetAngleTo(SelectedItem.Coordinate);
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
