using PortManager.Helpers;
using PortManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace PortManager.Views
{
    /// <summary>
    /// Interaction logic for PortDetailsWindow.xaml
    /// </summary>
    public partial class PortDetailsWindow : Window
    {
        public RelayCommand<int> KillCommand { get; }
        public PortDetailsWindow(PortInfoDetail details, RelayCommand<int> killCommand)
        {
            InitializeComponent();
            DataContext = details;
            KillCommand = killCommand;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExecutablePath_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string path = e.Uri.LocalPath;

            if (File.Exists(path))
            {
                Process.Start("explorer.exe", $"/select,\"{path}\"");
            }
            else
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            e.Handled = true;
        }
    }
}

