using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WeberLibraryFramework.Helper
{
#pragma warning disable SYSLIB0011 // 类型或成员已过时
    /// <summary>
    /// 序列化对象帮助类
    /// </summary>
    public static class ObjectSerializerHelper
    {
        /// <summary>
        /// 将指定的对象序列化为二进制数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(memoryStream, obj);

                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// 从二进制数组反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] bytes) where T : class
        {
            if (bytes == null)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// 从二进制数组反序列化枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="result">如果二进制数组是null，则为false；否则为true</param>
        /// <returns></returns>
        public static T DeserializeEnum<T>(byte[] bytes, out bool result) where T : Enum
        {
            result = true;
            if (bytes == null)
            {
                result = false;
                return default;
            }
            using (var memoryStream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
#pragma warning restore SYSLIB0011 // 类型或成员已过时
}
