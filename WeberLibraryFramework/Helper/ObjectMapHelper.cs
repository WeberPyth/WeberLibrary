using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeberLibraryFramework.Helper
{
    /// <summary>
    /// 对象同名属性字段映射帮助类
    /// </summary>
    /// <typeparam name="TIn">输入类的泛型</typeparam>
    /// <typeparam name="TOut">输出类的泛型</typeparam>
    public class ObjectMapHelper<TIn, TOut>
    {
        private static Func<TIn, TOut> _func = null;
        static ObjectMapHelper()
        {
            // 构建入参表达式
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> members = new List<MemberBinding>();
            // 从输入类构建属性集
            var properties = typeof(TOut).GetProperties().Join(typeof(TIn).GetProperties(), x => x.Name, y => y.Name, (x, y) => x)
                .Select((x) =>
                {
                    var objProperty = typeof(TIn).GetProperty(x.Name);
                    if (objProperty == null) 
                    {
                        throw new NullReferenceException();
                    }
                    MemberExpression property = Expression.Property(parameterExpression, objProperty);
                    MemberBinding memberBinding = Expression.Bind(x, property);
                    return memberBinding;
                });
            // 从输入类构建字段集
            var fields = typeof(TOut).GetFields().Join(typeof(TIn).GetFields(), x => x.Name, y => y.Name, (x, y) => x)
                .Select((x) =>
                {
                    var objField = typeof(TIn).GetField(x.Name);
                    if (objField == null) 
                    {
                        throw new NullReferenceException();
                    }
                    MemberExpression field = Expression.Field(parameterExpression, objField);
                    MemberBinding memberBinding = Expression.Bind(x, field);
                    return memberBinding;
                });
            // 所有的成员集合
            members = members.Concat(properties).Concat(fields).ToList();
            // 初始化成员
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), members.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
                {
                        parameterExpression
                });
            // 编译表达式
            _func = lambda.Compile();
        }
        /// <summary>
        /// 映射对象的同名属性字段到新的对象
        /// </summary>
        /// <param name="t">需要被映射的对象</param>
        /// <returns>映射结果</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static TOut MapObj(TIn t)
        {
            if (_func == null) 
            {
                throw new InvalidOperationException("Invalid Object Operation. ObjectMapHelper only support class type. Please check your args.");
            }
            return _func(t);
        }
    }
}
