using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManager.Models
{
    public class PortInfo
    {
        // always displayed
        public int PID { get; set; } = -1;
        public int Port { get; set; } = -1;
        public string ProcessName { get; set; } = "N/A";
        public string Protocol { get; set; } = "N/A";
        public string LocalAddress { get; set; } = "N/A";
        public string State { get; set; } = "N/A";

        // details page
        public string ExecutablePath { get; set; } = "N/A";
        public string RemoteAddress { get; set; } = "N/A";
        public string UserName { get; set; } = "N/A";
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public double MemoryMB { get; set; } = 0.0;
    }
}
