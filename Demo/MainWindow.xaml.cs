using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}


		private void SetPivotCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			AppViewModel vm = e.Parameter as AppViewModel;

			e.Handled = true;
			if (vm == null) return;
			e.CanExecute = vm.SelectedItem != null;
		}

		private void SetPivotCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			AppViewModel vm = e.Parameter as AppViewModel;

			vm.Pivot = vm.SelectedItem;
		}

	}
}
