using System;
using System.Runtime.InteropServices;

namespace WeberLibraryFramework.Helper
{
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
    /// <summary>
    /// Windows消息帮助类
    /// </summary>
    public class WindowsMessageHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        /// <summary>
        /// 通过窗口标题发送消息
        /// </summary>
        /// <param name="wTitle">窗口标题</param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public static bool PostMessageByWindowsTitle(string wTitle, uint msg, int wParam, int lParam = 0) 
        {

            IntPtr windowHandle = FindWindow(null, wTitle);

            if (windowHandle == IntPtr.Zero)
            {
                return false;
            }
            var result = PostMessage(windowHandle, msg, wParam, lParam);
            return false;
        }

    }
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
}
