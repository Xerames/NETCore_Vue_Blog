using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                Task result = invocation.ReturnValue as Task;
                result?.Wait();
                if (result?.IsFaulted ?? false)
                    throw result.Exception?.InnerException ?? result.Exception;
            }
            catch (Exception e)
            {
                isSuccess = false;
                if (invocation.ReturnValue is Task result && result.IsFaulted)
                    OnException(invocation, result.Exception?.InnerException ?? result.Exception ?? e);
                else
                    OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
