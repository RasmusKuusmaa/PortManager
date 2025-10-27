using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManager.Models
{
    public class PortInfoMain
    {
        public int PID { get; set; } = -1;
        public int Port { get; set; } = -1;
        public string ProcessName { get; set; } = "N/A";
        public string Protocol { get; set; } = "N/A";
        public string LocalAddress { get; set; } = "N/A";
        public string RemoteAddress { get; set; } = "N/A";
        public string State { get; set; } = "N/A";
    }
}
