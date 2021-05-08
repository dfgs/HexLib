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
		public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(AppViewModel), new PropertyMetadata(1, DemoModePropertyChanged));
		public int Count
		{
			get { return (int)GetValue(CountProperty); }
			set { SetValue(CountProperty, value); }
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
					if ((r & 1) != 0) item.Background = "WhiteSmoke";
					else item.Background = "White";
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
				case Demo.DemoModes.Angle:
					OnUpdateHexContentWithAngles();
					break;
				case Demo.DemoModes.Distance:
					OnUpdateHexContentWithDistances();
					break;
				case Demo.DemoModes.JumpTransform:
					OnUpdateHexContentWithJumpTransform();
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
					OnUpdateHexContentWithDistances();
					break;
				case Demo.DemoModes.Angle:
					OnUpdateHexContentWithAngles();
					break;
				case Demo.DemoModes.JumpTransform:
					OnUpdateHexContentWithJumpTransform();
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

		protected void OnUpdateHexContentWithAngles()
		{

			foreach (HexViewModel hex in HexMap)
			{
				if (SelectedItem == null) hex.Content = null;
				else
				{
					hex.Content= SelectedItem.Coordinate.GetAngleTo(hex.Coordinate);
				}
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
		protected void OnUpdateHexContentWithDistances()
		{
			object result;

			foreach (HexViewModel hex in HexMap)
			{
				if (SelectedItem == null) hex.Content = null;
				else
				{
					//result = SelectedItem.Coordinate.GetTaxiDriverDistanceTo(hex.Coordinate) + " / " + HexMap.GetDistance(SelectedItem.Coordinate, hex.Coordinate);
					result =  HexMap.GetDistance(SelectedItem.Coordinate, hex.Coordinate);
					//if (result == -1) hex.Content = null;
					hex.Content = result;
				}
			}
		}

		protected void OnUpdateHexContentWithJumpTransform()
		{
			object result;
			HexCoordinate[] results;

			foreach (HexViewModel hex in HexMap)
			{
				if (SelectedItem == null) hex.Content = null;
				else
				{
					if (hex == SelectedItem)
					{
						results = SelectedItem.Coordinate.JumpTransform(Count).ToArray();
						result = results.First().Radius.ToString() + "," + string.Join("-", results.Select(item=>item.RingIndex) );
						//result = results.FirstOrDefault().Radius.ToString() + "," + results.FirstOrDefault().RingIndex+"-" + results.LastOrDefault().RingIndex;
					}
					else result = hex.Coordinate;
					hex.Content = result;
				}
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
