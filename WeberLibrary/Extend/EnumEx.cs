using System;
using System.Linq.Expressions;

namespace WeberLibrary.Extend
{
    /// <summary>
    /// 枚举表达式生成扩展类
    /// </summary>
    /// <typeparam name="TIn">输入泛型</typeparam>
    /// <typeparam name="TOut">输出泛型</typeparam>
    public class EnumExpressionGenericMapper<TIn, TOut> where TIn : Enum
    {

        private static Func<TIn, TIn, TIn>? _insFunc = null;
        private static Func<TIn, TIn, byte, bool>? _hasInsFunc = null;
        private static Func<TIn, TIn, TIn>? _unionFunc = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static EnumExpressionGenericMapper()
        {
            // 构造入参表达式
            ParameterExpression o = Expression.Parameter(typeof(TIn), "o");
            ParameterExpression t = Expression.Parameter(typeof(TIn), "t");
            ParameterExpression b = Expression.Parameter(typeof(byte), "b");
            var baseType = Enum.GetUnderlyingType(typeof(TIn));
            var convO = Expression.Convert(o, baseType);
            var convT = Expression.Convert(t, baseType);
            if (baseType == typeof(int) || baseType == typeof(uint)
                || baseType == typeof(short) || baseType == typeof(ushort)
                || baseType == typeof(long) || baseType == typeof(ulong)
                || baseType == typeof(byte) || baseType == typeof(sbyte)
                )
            {
                var andExpression = Expression.And(convO, convT);

                // 构造判断交集的表达式树
                var convB = Expression.Convert(b, baseType);
                var equalExpression = Expression.Equal(andExpression, convB);
                _hasInsFunc = Expression.Lambda<Func<TIn, TIn, byte, bool>>(equalExpression, o, t, b).Compile();
                // 构造获取交集的表达式树
                var convR = Expression.Convert(andExpression, typeof(TIn));
                _insFunc = Expression.Lambda<Func<TIn, TIn, TIn>>(convR, o, t).Compile();

                // 构造获取并集的表达式树
                var orExpression = Expression.Or(convO, convT);
                var convOR_R = Expression.Convert(orExpression, typeof(TIn));
                _unionFunc = Expression.Lambda<Func<TIn, TIn, TIn>>(convOR_R, o, t).Compile();

                // 


            }
        }
        /// <summary>
        /// 获取交集
        /// </summary>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static TIn GetIns(TIn o, TIn t)
        {
            if (_insFunc == null) 
            {
                throw new InvalidOperationException("Expression is null. Maybe cause you used Enum with wrong base type.");
            }
            return _insFunc(o, t);
        }
        /// <summary>
        /// 判断二者是否拥有交集
        /// </summary>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static bool HasIns(TIn o, TIn t)
        {
            if (_hasInsFunc == null)
            {
                throw new InvalidOperationException("Expression is null. Maybe cause you used Enum with wrong base type.");
            }
            return !_hasInsFunc(o, t, 0);
        }
        /// <summary>
        /// 获取并集
        /// </summary>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static TIn GetUnion(TIn o, TIn t)
        {
            if (_unionFunc == null)
            {
                throw new InvalidOperationException("Expression is null. Maybe cause you used Enum with wrong base type.");
            }
            return _unionFunc(o, t);
        }
        /// <summary>
        /// 判断t是否为o的子集
        /// </summary>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static bool IsSubset(TIn o, TIn t) 
        {
            TIn cache = GetIns(o, t);

            return cache.Equals(o);
        }
    }
    /// <summary>
    /// 枚举类扩展
    /// </summary>
    public static class WeberEnumEx 
    {
        /// <summary>
        /// 判断二者是否存在交集
        /// </summary>
        /// <typeparam name="T">目标枚举的类型</typeparam>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static bool HasIns<T>(this T o, T t)where T: Enum 
        {
            return EnumExpressionGenericMapper<T, T>.HasIns(o, t);
        }

        /// <summary>
        /// 判断目标值是否为原值的子集
        /// </summary>
        /// <typeparam name="T">目标枚举的类型</typeparam>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static bool HasSubset<T>(this T o, T t) where T : Enum
        {
            return EnumExpressionGenericMapper<T, T>.IsSubset(o, t);
        }

        /// <summary>
        /// 获取二者交集
        /// </summary>
        /// <typeparam name="T">目标枚举的类型</typeparam>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static T GetIns<T>(this T o, T t) where T : Enum
        {
            return EnumExpressionGenericMapper<T, T>.GetIns(o, t);
        }

        /// <summary>
        /// 获取二者并集
        /// </summary>
        /// <typeparam name="T">目标枚举的类型</typeparam>
        /// <param name="o">原始值</param>
        /// <param name="t">目标值</param>
        /// <returns>运算结果</returns>
        public static T GetUnion<T>(this T o, T t) where T : Enum
        {
            return EnumExpressionGenericMapper<T, T>.GetUnion(o, t);
        }
    }
}
