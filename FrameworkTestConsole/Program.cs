using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WeberLibraryFramework.Extend;
using WeberLibraryFramework.Helper;

namespace FrameworkTestConsole
{
    [System.Flags]
    public enum MyEnumType 
    {
        Alpha = 1,
        Beta = 1 << 1,
        Gamma = 1 << 2,
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string origin = "Hello, World!";
            string key = "WeberLibrary";
            string iv = "Weber";
            var b64str = AESHelper.Encrypt(origin, key, iv);
            var rs = AESHelper.Decrpt(b64str, key, iv);
            Console.WriteLine(b64str);
            Console.WriteLine(rs);

            Console.ReadLine();
        }
    }
}
