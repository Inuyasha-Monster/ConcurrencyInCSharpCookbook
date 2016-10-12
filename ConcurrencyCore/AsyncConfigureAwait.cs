using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class AsyncConfigureAwait
    {
        async Task DoSomethingAsync()
        {
            int val = 13;
            // 异步方法等待1秒
            await Task.Delay(TimeSpan.FromSeconds(1));

            //使用 ConfiguraeAwait（fasle）显式在接下来的同步块中用线程池上下文执行
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(continueOnCapturedContext: false);

            val *= 2;

            await Task.Delay(TimeSpan.FromSeconds(1));
            Trace.WriteLine(val);

            // 适合大部分I/O型任务
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
        }
        // 代码分析(1)：
        // 问：  一个async方法是由多个同步执行的程序块组成，每个同步程序块由await分割。第一个同步块在调用者线程执行，但其他同步块在哪里执行？
        // 答： 常见情况是，await语句等待一个任务完成时，当方法在await暂停时，就可以捕捉到上下文。如果当前上下文SynchronizationContext不为空，那么捕获的上下文就是SynchronizationContext上下文，否则就是当前的TaskScheduler。一般来说，运行在UI线程时采用UI上下文，处理asp.net请求时候就是asp.net的请求上下文，其他情况采用的时线程池的上下文执行同步块。

        // 问：ConfigureAwait 何时使用问题？
        // 答案： 最好的做法，在代码核心库一直使用 ConfigureAwait(continueOnCapturedContext: false)。在外围的用户界面代码中，只是在需要的时候恢复上下文。
    }
}
