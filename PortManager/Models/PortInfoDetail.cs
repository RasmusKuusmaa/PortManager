using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManager.Models
{
    public class PortInfoDetail : PortInfoMain
    {
        public string ExecutablePath { get; set; }
        public string RemoteAddress { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public double MemoryMB { get; set; }
    }
}
