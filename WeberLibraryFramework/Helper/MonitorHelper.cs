using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace WeberLibraryFramework.Helper
{
    /// <summary>
    /// 显示器信息帮助类
    /// </summary>
    public class MonitorHelper
    {
        /// <summary>
        /// 获取屏幕dpi
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint GetDpiForSystem();

        /// <summary>
        /// 矩形结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// 左
            /// </summary>
            public int left;
            /// <summary>
            /// 上
            /// </summary>
            public int top;
            /// <summary>
            /// 右
            /// </summary>
            public int right;
            /// <summary>
            /// 下
            /// </summary>
            public int bottom;
        }

        /// <summary>
        /// 显示器信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public int cbSize;
            /// <summary>
            /// 显示器区域
            /// </summary>
            public RECT rcMonitor;
            /// <summary>
            /// 工作区域
            /// </summary>
            public RECT rcWork;
            /// <summary>
            /// 一组表示显示监视器属性的标志
            /// </summary>
            public int dwFlags;
        }

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        private static bool MonitorEnumCallback(IntPtr monitor, IntPtr hdc, ref RECT rect, IntPtr data)
        {
            MONITORINFO monitorInfo = new MONITORINFO();
            monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            bool success = GetMonitorInfo(monitor, ref monitorInfo);

            if (success)
            {
                ms.Add(monitorInfo);
                //Console.WriteLine($"Work Area: {monitorInfo.rcWork.left}, {monitorInfo.rcWork.top}, {monitorInfo.rcWork.right}, {monitorInfo.rcWork.bottom}");
            }

            return true; // Return true to continue enumeration
        }

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        private static List<MONITORINFO> ms = new List<MONITORINFO>();

        /// <summary>
        /// 获取所有显示器信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 使用实示例
        /// var rs = MonitorHelper.GetMonitors();
        /// foreach (var m in rs) 
        /// {
        ///     Console.WriteLine($"{m.rcWork.left} {m.rcWork.top} {m.rcWork.right} {m.rcWork.bottom}");
        /// }
        /// </remarks>
        public static IEnumerable<MONITORINFO> GetMonitors()
        {
            ms.Clear();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumCallback, IntPtr.Zero);

            return ms.Concat(new List<MONITORINFO>());
        }

        /// <summary>
        /// 获取所有显示器信息(从DPI换算后)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RECT> GetOriginMonitors(bool getWorkArea = false)
        {
            uint dpiX = GetDpiForSystem();
            double mul = dpiX / 96d;
            GetMonitors();
            var rs = ms.Select((x) =>
            {
                RECT rect = new RECT();
                if (getWorkArea)
                {
                    rect.left = (int)(x.rcWork.left / mul);
                    rect.top = (int)(x.rcWork.top / mul);
                    rect.right = (int)(x.rcWork.right / mul);
                    rect.bottom = (int)(x.rcWork.bottom / mul);
                    return rect;
                }
                rect.left = (int)(x.rcMonitor.left / mul);
                rect.top = (int)(x.rcMonitor.top / mul);
                rect.right = (int)(x.rcMonitor.right / mul);
                rect.bottom = (int)(x.rcMonitor.bottom / mul);
                return rect;
            });
            return rs;
        }

    }
}
