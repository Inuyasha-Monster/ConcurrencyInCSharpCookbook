using ConcurrencyCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ShowThreadId();
            //AsyncException.TryTestExceptionAsync().Wait();
            //ShowThreadId();

            Console.WriteLine("---------测试Async捕获上下文机制-----------");
            ConfigureAwaitTestAsync().Wait();
            Console.ReadKey();
        }

        static void ShowThreadId()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        static async Task ConfigureAwaitTestAsync()
        {
            // console 模式模式下面 默认应该是线程池模式
            ShowThreadId();
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            ShowThreadId();
        }
    }
}
