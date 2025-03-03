using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkTestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity");
            var rs = searcher.Get();
            List<ManagementBaseObject> ls = new List<ManagementBaseObject>();
            foreach (var t in rs)
            {
                ls.Add(t);
            }
            ls.Sort();
        }
    }
}
