using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManager.Models
{
    public class PortInfoMain
    {
        public int PID { get; set; }
        public int Port { get; set; }
        public string ProcessName { get; set; }
        public string Protocol { get; set; }
        public string LocalAddress { get; set; }
        public string State { get; set; }
    }
}
