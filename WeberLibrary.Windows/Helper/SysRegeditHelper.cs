using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace WeberLibrary.Windows.Helper
{
#pragma warning disable CA1416 // 验证平台兼容性
    /// <summary>
    /// 注册表读写帮助类
    /// </summary>
    public static class WindowsRegeditHelper
    {
        /// <summary>
        /// 写入注册表（CurrentUser节点）
        /// </summary>
        /// <param name="path">注册表路径</param>
        /// <param name="keyValuePair">写入的键值对</param>
        /// <param name="registryValueKind">值的类型</param>
        public static void WriteToRegistry(string path, KeyValuePair<string, object> keyValuePair, RegistryValueKind registryValueKind = RegistryValueKind.Unknown)
        {

            RegistryKey hkcu = Registry.CurrentUser;

            WriteToRegistry(path, keyValuePair, hkcu, registryValueKind);
        }

        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="path">注册表路径</param>
        /// <param name="keyValuePair">写入的键值对</param>
        /// <param name="root">顶级节点</param>
        /// <param name="registryValueKind">值的类型</param>
        public static void WriteToRegistry(string path, KeyValuePair<string, object> keyValuePair, RegistryKey root, RegistryValueKind registryValueKind = RegistryValueKind.Unknown)
        {

            using (var sw = root.CreateSubKey(path))
            {
                if (sw == null)
                {
                    throw new Exception("Error: registry key create failed.");
                }
                sw.SetValue(keyValuePair.Key, keyValuePair.Value, registryValueKind);
            }
        }
        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <typeparam name="T">要读取的对象类型</typeparam>
        /// <param name="path">注册表路径</param>
        /// <param name="key">键</param>
        /// <returns>如果读取失败，返回null</returns>
        public static T ReadFromRegistry<T>(string path, string key) where T : class
        {
            RegistryKey hkcu = Registry.CurrentUser;
            using (var sw = hkcu.OpenSubKey(path))
            {
                if (sw == null)
                {
                    return null;
                }
                object result = sw.GetValue(key);
                return result == null ? null : (T)result;
            }
        }

        /// <summary>
        /// 删除注册表
        /// </summary>
        /// <param name="path">注册表路径</param>
        /// <param name="key">键</param>
        /// <exception cref="Exception"></exception>
        public static void DeleteRegistry(string path, string key)
        {
            RegistryKey hkcu = Registry.CurrentUser;
            using (var sw = hkcu.OpenSubKey(path))
            {
                if (sw == null)
                {
                    throw new Exception("Error: registry key delete failed.");
                }
                sw.DeleteValue(key, false);
            }
        }
    }
#pragma warning restore CA1416 // 验证平台兼容性
}
