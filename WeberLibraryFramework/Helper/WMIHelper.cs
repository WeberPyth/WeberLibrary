using System.Collections.Generic;
using System.Management;
using System.Runtime.CompilerServices;

namespace WeberLibraryFramework.Helper
{
    /// <summary>
    /// WMI查询结果类
    /// </summary>
    public class WMIResultObject
    {
        /// <summary>
        /// 
        /// </summary>
        public ManagementBaseObject BaseObject { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public WMIResultObjectProperties WMIProperties { get; internal set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class WMIResultObjectProperties
    {
        internal WMIResultObjectProperties(PropertyDataCollection collection) 
        {
            _propertyCollection = collection;
        }

        private PropertyDataCollection _propertyCollection;

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Availability { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Caption { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData ClassGuid { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData CompatibleID { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData ConfigManagerErrorCode { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData ConfigManagerUserConfig { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData CreationClassName { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Description { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData DeviceID { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData ErrorCleared { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData ErrorDescription { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData HardwareID { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData InstallDate { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData LastErrorCode { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Manufacturer { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Name { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData PNPClass { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData PNPDeviceID { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData PowerMaqnagementCapabilities { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData PowerManagementSupported { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Present { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Service { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData Status { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData StatusInfo { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData SystemCreationClassName { get => Transform(); }

        /// <summary>
        /// 
        /// </summary>
        public PropertyData SystemName { get => Transform(); }


        internal PropertyData Transform(PropertyDataCollection properties, [CallerMemberName] string propertyName = null) 
        {
            if (properties == null) 
            {
                return null;
            }
            try
            {
                return properties[propertyName];
            }
            catch 
            {

            }
            return null;
        }

        internal PropertyData Transform([CallerMemberName] string propertyName = null) 
        {
            return Transform(_propertyCollection, propertyName);
        }
    }

    /// <summary>
    /// WMI查询帮助类
    /// </summary>
    public class WMIHelper
    {
        /// <summary>
        /// 获取WMI查询信息
        /// </summary>
        /// <param name="wmiSql">WMI查询语句</param>
        /// <returns></returns>
        public static IEnumerable<WMIResultObject> Get(string wmiSql = "select * from Win32_PnPEntity") 
        {
            var searcher = new ManagementObjectSearcher(wmiSql);
            var rs = searcher.Get();
            List<WMIResultObject> ls = new List<WMIResultObject>();
            foreach (var target in rs)
            {
                var cache = ManagementBaseObjectParser(target);
                ls.Add(cache);
            }
            return ls;
        }

        /// <summary>
        /// 转化为WMI查询结果对象
        /// </summary>
        /// <param name="baseObject"></param>
        /// <returns></returns>
        public static WMIResultObject ManagementBaseObjectParser(ManagementBaseObject baseObject) 
        {
            var cache = new WMIResultObject();
            cache.BaseObject = baseObject.Clone() as ManagementBaseObject;
            cache.WMIProperties = new WMIResultObjectProperties(cache.BaseObject.Properties);
            return cache;
        }
    }
}
