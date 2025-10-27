using PortManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortManager.Services
{
    public static class PortService
    {
        public static List<PortInfoMain> FetchAllPorts()
        {
            var ports = new List<PortInfoMain>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netstat",
                    Arguments = "-ano",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                              .Skip(4);

            var regex = new Regex(@"^(TCP|UDP)\s+([\d\.:]+):(\d+)\s+([\d\.:]+):(\*|\d+)?\s*(\S*)\s+(\d+)$",
                                  RegexOptions.IgnoreCase);

            foreach (var line in lines)
            {
                var match = regex.Match(line.Trim());
                if (!match.Success) continue;

                string protocol = match.Groups[1].Value;
                string localAddr = match.Groups[2].Value;
                int localPort = int.Parse(match.Groups[3].Value);
                string remoteAddr = match.Groups[4].Value + ":" + match.Groups[5].Value;
                string state = string.IsNullOrWhiteSpace(match.Groups[6].Value) ? "N/A" : match.Groups[6].Value;
                int pid = int.Parse(match.Groups[7].Value);

                string processName = "N/A";
                try { processName = Process.GetProcessById(pid).ProcessName; }
                catch { }

                ports.Add(new PortInfoMain
                {
                    PID = pid,
                    Port = localPort,
                    Protocol = protocol,
                    LocalAddress = localAddr,
                    State = state,
                    ProcessName = processName
                });
            }

            return ports.OrderBy(p => p.Port).ToList();
        }

     
    }
}
