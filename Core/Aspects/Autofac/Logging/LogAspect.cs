using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Abstract;
using Core.Utilities.Messages;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private Type _loggingType;

        public LogAspect(Type loggerType)
        {
            if (!typeof(ILogger).IsAssignableFrom(loggerType))
            {
                throw new System.Exception(AspectMessages.NotLoggingClass);
            }

            _loggingType = loggerType;
        }

        private string GetExceptionDetail(IInvocation invocation, Exception e)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var data = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))}) -> Error: {e.Message} : {DateTime.Now}\n";
            return data;
        }

        private string GetSuccessDetail(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var data = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))}) -> Result: {invocation.ReturnValue} : {DateTime.Now}\n";
            return data;
        }

        protected override void OnException(IInvocation invocation, Exception e)
        {
            var logger = (ILogger)Activator.CreateInstance(_loggingType);
            var data = GetExceptionDetail(invocation, e);
            logger.Log(data);
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            var logger = (ILogger)Activator.CreateInstance(_loggingType);
            var data = GetSuccessDetail(invocation);
            logger.Log(data);
        }
    }
}
