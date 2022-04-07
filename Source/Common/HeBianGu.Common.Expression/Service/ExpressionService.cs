// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace HeBianGu.Common.Expression
{
    public class ExpressionService
    {
        #region - Base -


        /// <summary>
        /// 根据参数动态生成Func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(string expression, Type resultType, params ParameterExpression[] parameters)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, null);

            return System.Linq.Expressions.Expression.Lambda(parser.Parse(resultType), parameters);
        }

        /// <summary>
        /// 根据参数动态生成Func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda<TResult>(string expression, params ParameterExpression[] parameters)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, null);

            return System.Linq.Expressions.Expression.Lambda(parser.Parse(typeof(TResult)), parameters);
        }

        /// <summary>
        /// 转换成Delegate 带默认参数 arg arg1 arg2
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static TDelegate ParseDelegate<TDelegate>(string expression) where TDelegate : Delegate
        {
            List<ParameterExpression> parameters = new List<ParameterExpression>();

            MethodInfo method = typeof(TDelegate).GetMethod("Invoke");

            ParameterInfo[] methodParameters = method.GetParameters();

            foreach (ParameterInfo item in methodParameters)
            {
                parameters.Add(System.Linq.Expressions.Expression.Parameter(item.ParameterType, item.Name));
            }

            ExpressionParser parser = new ExpressionParser(parameters?.ToArray(), expression, null);

            return System.Linq.Expressions.Expression.Lambda(parser.Parse(typeof(TDelegate).GetMethod("Invoke").ReturnType), parameters).Compile() as TDelegate;
        }

        /// <summary>
        /// 根据参数动态生成Func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda<T, TResult>(string expression, params Parameter<T>[] parameters)
        {
            return ParseLambda<TResult>(expression, parameters?.Select(l => l.Expression).ToArray());
        }

        /// <summary>
        /// 根据参数动态生成Func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static LambdaExpression ParseLambdaLambdaExpression<TResult>(string expression, params IParameter[] parameters)
        {
            return ParseLambda<TResult>(expression, parameters?.Select(l => l.Expression).ToArray());
        }

        #endregion

        /// <summary>
        /// Func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Func<TResult> ParseFunc<TResult>(string expression)
        {
            return ParseLambda<TResult>(expression).Compile() as Func<TResult>;
        }

        /// <summary>
        /// Func<T, TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Func<T, TResult> ParseFunc<T, TResult>(string expression, Parameter<T> parameter)
        {
            return ParseLambda<TResult>(expression, new ParameterExpression[] { parameter.Expression }).Compile() as Func<T, TResult>;
        }

        /// <summary>
        /// Func<T1, T2, TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Func<T1, T2, TResult> ParseFunc<T1, T2, TResult>(string expression, Parameter<T1> parameter1, Parameter<T2> parameter2)
        {
            ParameterExpression[] parameters = new ParameterExpression[] { parameter1.Expression, parameter2.Expression };

            return ParseLambda<TResult>(expression, parameters).Compile() as Func<T1, T2, TResult>;
        }

        /// <summary>
        /// Func<T1, T2, T3, TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Func<T1, T2, T3, TResult> ParseFunc<T1, T2, T3, TResult>(string expression, Parameter<T1> p1, Parameter<T2> p2, Parameter<T3> p3)
        {
            ParameterExpression[] parameters = new ParameterExpression[] { p1.Expression, p2.Expression, p3.Expression };

            return ParseLambda<TResult>(expression, parameters).Compile() as Func<T1, T2, T3, TResult>;
        }

        /// <summary>
        /// Func<T1, T2, T3, T4, TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Func<T1, T2, T3, T4, TResult> ParseFunc<T1, T2, T3, T4, TResult>(string expression, Parameter<T1> p1, Parameter<T2> p2, Parameter<T3> p3, Parameter<T4> p4)
        {
            ParameterExpression[] parameters = new ParameterExpression[] { p1.Expression, p2.Expression, p3.Expression, p4.Expression };

            return ParseLambda<TResult>(expression, parameters).Compile() as Func<T1, T2, T3, T4, TResult>;
        }
    }
}
