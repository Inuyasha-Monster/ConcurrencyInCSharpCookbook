using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class AsyncCancellationToken
    {
        public static async Task IssueCancellRequest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = CancelableMethodAsync(cts.Token);
            // 这里，操作在正在运行
            await Task.Delay(TimeSpan.FromSeconds(5));// 模拟运行其他同步块代码
            cts.Cancel();

            // （异步地）等到操作结束
            try
            {
                await task;
                // 如果运行到这里，说明操作取消请求生效前，操作正在完成
                Console.WriteLine("如果运行到这里，说明操作取消请求生效前，操作正常完成");
            }
            catch (OperationCanceledException ex)
            {
                // 如果运行到这里，说明操作在完成前被取消
                Console.WriteLine("如果运行到这里，说明操作在完成前被取消");
                Console.WriteLine(ex.Message + " ex.CancellationToken.CanBeCanceled:" + ex.CancellationToken.CanBeCanceled.ToString() + " ex.CancellationToken.IsCancellationRequested: " + ex.CancellationToken.IsCancellationRequested.ToString());

            }
            catch (Exception ex)
            {
                // 如果运行到这里，说明取消请求生效之前，操作出错结束
                Console.WriteLine("如果运行到这里，说明取消请求生效之前，操作出错结束" + ex.Message);
            }
        }

        private static async Task CancelableMethodAsync(CancellationToken token)
        {
            // 如果是这里抛出了异常，表示会通知调用者线程
            //throw new Exception("CancelableMethodAsync invoke error");
            await Task.Delay(TimeSpan.FromSeconds(3), token);
            throw new Exception("CancelableMethodAsync invoke error");
        }
    }
}
