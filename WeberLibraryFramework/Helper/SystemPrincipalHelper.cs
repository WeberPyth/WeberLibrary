using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WeberLibraryFramework.Helper
{
#pragma warning disable CA1416 // 验证平台兼容性
    /// <summary>
    /// 权限组检查帮助类
    /// </summary>
    public class SystemPrincipalHelper
    {
        /// <summary>
        /// 权限组检查
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <remarks>
        /// 默认检查是否为管理员权限组，仅适用于Windows系统
        /// </remarks>
        public static bool PrincipalCheck(WindowsBuiltInRole role = WindowsBuiltInRole.Administrator)

        {
            var identity = WindowsIdentity.GetCurrent();
            var prc = new WindowsPrincipal(identity);

            return prc.IsInRole(role);
        }
    }
#pragma warning restore CA1416 // 验证平台兼容性
}
