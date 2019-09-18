/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ExtException
{
    /// <summary>
    /// 使用try..catch..捕获异常
    /// </summary>
    public static class TryCatch
    {
        /// <summary>
        /// 默认异常处理方式
        /// </summary>
        public static ICatchAction DefaultCatchAction = new CatchAction(Console.WriteLine);//MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        /// <summary>
        /// 运行并捕获指定方法可能出现的异常，以<see cref="DefaultCatchAction"/>的方式处理异常
        /// </summary>
        /// <typeparam name="T">方法的返回值</typeparam>
        /// <param name="func">需要执行和捕获异常的有返回值的方法</param>
        /// <param name="finallyAction">在finally中执行的方法，可赋值为null</param>
        /// <returns></returns>
        public static T TryRun<T>(Func<T> func, Action finallyAction)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                DefaultCatchAction.Invoke(ex);
                return default(T);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
        /// <summary>
        /// 运行并捕获指定方法可能出现的异常，以指定方式处理异常
        /// </summary>
        /// <typeparam name="T">方法的返回值</typeparam>
        /// <param name="func">需要执行和捕获异常的有返回值的方法</param>
        /// <param name="catchActon">异常处理方式</param>
        /// <param name="finallyAction">在finally中执行的方法，可赋值为null</param>
        /// <returns></returns>
        public static T TryRun<T>(Func<T> func, ICatchAction catchActon, Action finallyAction)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                catchActon.Invoke(ex);
                return default(T);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
        /// <summary>
        /// 运行并捕获指定方法可能出现的异常，以<see cref="DefaultCatchAction"/>的方式处理异常
        /// </summary>
        /// <param name="action">需要执行和捕获异常的无返回值的方法</param>
        /// <param name="finallyAction">在finally中执行的方法，可赋值为null</param>
        /// <returns></returns>
        public static void TryRun(Action action, Action finallyAction)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                DefaultCatchAction.Invoke(ex);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
        /// <summary>
        /// 运行并捕获指定方法可能出现的异常，以指定方式处理异常
        /// </summary>
        /// <param name="action">需要执行和捕获异常的无返回值的方法</param>
        /// <param name="catchActon">异常处理方式</param>
        /// <param name="finallyAction">在finally中执行的方法，可赋值为null</param>
        /// <returns></returns>
        public static void TryRun(Action action, ICatchAction catchActon, Action finallyAction)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                DefaultCatchAction.Invoke(ex);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
    }
}
