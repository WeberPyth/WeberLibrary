using System.Linq;
using System.Reflection;
using WeberLibrary.Extend;
using WeberLibrary.Helper;

namespace NETTestConsole
{
    internal class Program
    {
        public class Tom 
        {
            public string name;
            public int age;
        }
        public class Jack
        {
            public string name;
            public int age;
        }
        [System.Flags]
        public enum MyEnumType
        {
            Alpha = 1,
            Beta = 1 << 1,
            Gamma = 1 << 2,
        }
        static void Main(string[] args)
        {
            // ChunkBy
            IEnumerable<int> myList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var chunkResult = myList.ChunkBy(2);

            // Enum
            MyEnumType origin = MyEnumType.Alpha | MyEnumType.Beta;
            MyEnumType target = MyEnumType.Beta | MyEnumType.Gamma;


            // Usage: Any usage behavior will trigger the compile

            /*bool hasIns = origin.HasIns(target);
            bool hasSubset = origin.HasSubset(target);
            MyEnumType getIns = origin.GetIns(target);
            MyEnumType getUnion = origin.GetUnion(target);*/
            // Enum test
            DateTime startTime = DateTime.Now;
            startTime = DateTime.Now;
            // Execute 10,000,000 times
            for (int k = 0; k < 10_000_000; k++)
            {
                origin.HasFlag(target);
            }
            Console.WriteLine($"[Origin]HasFlag : {(DateTime.Now - startTime).TotalMilliseconds}ms");
            startTime = DateTime.Now;

            // Trigger compile: This time will cost 10+ms
            origin.HasIns(target);

            startTime = DateTime.Now;
            // Execute 10,000,000 times
            for (int k = 0; k < 10_000_000; k++)
            {
                origin.HasIns(target);
            }
            Console.WriteLine($"[Library]HasIns(after compiled) : {(DateTime.Now - startTime).TotalMilliseconds}ms");

            var tom = new Tom();
            tom.name = "Tom";
            tom.age = 20;
            Jack jack = new Jack();
            

            startTime = DateTime.Now;
            // Execute 10,000,000 times
            for (int k = 0; k < 10_000_000; k++)
            {
                MapObject(tom, ref jack);
            }
            Console.WriteLine($"[Ref]ObjMap : {(DateTime.Now - startTime).TotalMilliseconds}ms");

            // Trigger compile
            jack = ObjectMapHelper<Tom, Jack>.MapObj(tom);


            startTime = DateTime.Now;
            // Execute 10,000,000 times
            for (int k = 0; k < 10_000_000; k++)
            {
                jack = ObjectMapHelper<Tom, Jack>.MapObj(tom);
            }
            Console.WriteLine($"[Library]ObjectMapHelper: {(DateTime.Now - startTime).TotalMilliseconds}ms");

            Console.WriteLine($"{jack.name} {jack.age}");
            //jack = ObjectMapHelper<Tom, Jack>.MapObj(tom);



            Console.ReadLine();
        }


        public static void MapObject(Tom source,ref Jack destination)
        {
            // 获取源对象的类型信息
            Type sourceType = source.GetType();
            // 获取目标对象的类型信息
            Type destinationType = destination.GetType();

            // 遍历源对象的所有属性
            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                // 在目标对象中查找同名的属性
                PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // 确保源属性和目标属性类型相同或兼容
                    if (sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        // 读取源属性的值并设置到目标属性
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    }
                }
            }

            // 遍历源对象的所有属性
            foreach (FieldInfo sourceProperty in sourceType.GetFields())
            {
                // 在目标对象中查找同名的属性
                FieldInfo destinationProperty = destinationType.GetField(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.IsPublic)
                {
                    // 确保源属性和目标属性类型相同或兼容
                    if (sourceProperty.FieldType == destinationProperty.FieldType)
                    {
                        // 读取源属性的值并设置到目标属性
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                    }
                }
            }
        }
    }
}