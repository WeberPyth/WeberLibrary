using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WeberLibraryFramework.Extend;

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
            // ChunkBy
            IEnumerable<int> myList = new List<int> { 1,2,3,4,5,6,7,8,9,10 };
            var chunkResult = myList.ChunkBy(2);

            // Enum
            MyEnumType origin = MyEnumType.Alpha | MyEnumType.Beta;
            MyEnumType target = MyEnumType.Beta | MyEnumType.Gamma;
            bool hasIns = origin.HasIns(target);
        }
    }
}
