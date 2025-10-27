using PortManager.Helpers;
using PortManager.Models;
using PortManager.Services;
using PortManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PortManager.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        private ObservableCollection<PortInfoMain> _allports;

        public ObservableCollection<PortInfoMain> AllPorts
        {
            get { return _ports; }
            set
            {
                _ports = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<PortInfoMain> _ports;

        public ObservableCollection<PortInfoMain> Ports
        {
            get { return _ports; }
            set { _ports = value;
                OnPropertyChanged();
            }
        }
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value;
                OnPropertyChanged();
                FilterPorts();
            }       
        }

        public RelayCommand<int> KillCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand<PortInfoMain> ShowDetailsCommand { get; }
        public DashBoardViewModel()
        {
            _allports = new ObservableCollection<PortInfoMain>();
            Ports = new ObservableCollection<PortInfoMain>();
            KillCommand = new RelayCommand<int>(
               pid =>
               {
                   if (MessageBox.Show($"Kill process {pid}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                   {
                       PortService.KillProcess(pid);
                       LoadPorts();
                   }
               },
               pid => pid > 0 
            );
            ShowDetailsCommand = new RelayCommand<PortInfoMain>(port =>
            {
                if (port == null) return;

                var details = PortService.FetchPortDetails(port.PID);
                var detailsWindow = new PortDetailsWindow(details);
                detailsWindow.Owner = Application.Current.MainWindow;
                detailsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                detailsWindow.ShowDialog();
            });
            RefreshCommand = new RelayCommand(LoadPorts, () => true);
            LoadPorts();
        }
    
        private void LoadPorts()
        {
            var fetchedPorts = PortService.FetchAllPorts();
            foreach(var p in fetchedPorts)
            {
                _allports.Add(p);
            }
            FilterPorts();
        }

        private void FilterPorts()
        {
            Ports.Clear();
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allports

                : _allports.Where(p =>
                    p.ProcessName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    p.Port.ToString().Contains(SearchText) ||
                    p.Protocol.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var p in filtered)
                Ports.Add(p);
        }
    }
}
