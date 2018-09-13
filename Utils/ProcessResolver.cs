using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace LCU.NET.Utils
{
    public interface IProcessResolver
    {
        Process[] GetProcessesByName(string name);
        string GetCommandLine(Process process);
    }

    public class ProcessResolver : IProcessResolver
    {
        public string GetCommandLine(Process process)
        {
            using (var searcher = new ManagementObjectSearcher(
                $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}"))
            using (var objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        public Process[] GetProcessesByName(string name)
        {
            return Process.GetProcessesByName(name);
        }
    }
}
