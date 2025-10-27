using PortManager.Models;
using PortManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManager.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        private ObservableCollection<PortInfoMain> _ports;

        public ObservableCollection<PortInfoMain> Ports
        {
            get { return _ports; }
            set { _ports = value;
                OnPropertyChanged();
            }
        }

        public DashBoardViewModel()
        {
            Ports = new ObservableCollection<PortInfoMain>();
            LoadPorts();
        }

        private void LoadPorts()
        {
            Ports.Clear();
            var fetchedPorts = PortService.FetchAllPorts();
            foreach (var p in fetchedPorts)
                Ports.Add(p);
        }
    }
}
