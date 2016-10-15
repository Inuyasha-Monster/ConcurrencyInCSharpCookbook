using ConcurrencyCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
            // 测试异步异常
            //ShowThreadId();
            //AsyncException.TryTestExceptionAsync().Wait();
            //ShowThreadId();

            // 测试Async上下文捕获机制
            //Console.WriteLine("---------测试Async捕获上下文机制-----------");
            //ConfigureAwaitTestAsync().Wait();

            // 测试进度报告
            //AsyncIProgress.CallMyProgessAsync().Wait();

            // 测试WhenAll
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //AsyncWhenAll.TestWhenAllAsync().Wait();
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed.TotalSeconds + " s");

            //AsyncWhenAll.TestWhenAllResultIntArrayAsync().Result.ToList().ForEach(x => Console.WriteLine(x));
            AsyncWhenAll.DownloadAllUrlsHtmlAsync(new List<string>()
            {
                "http://www.baidu.com",
                "http://www.google.com"
            }).Result.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine("ok");



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

        /// <summary>
        /// 测试中断并行
        /// </summary>
        /// <param name="matrices"></param>
        /// <param name="degrees"></param>
        void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
        {
            Parallel.ForEach<Matrix>(matrices, x =>
            {
                x.Rotate(degrees);
            });

            // 测试 并行中断
            Parallel.ForEach<Matrix>(matrices, (Matrix x, ParallelLoopState state) =>
            {
                if (!x.IsInvertible)
                    state.Stop();
                else
                    x.Rotate(degrees);
            });
        }

        /// <summary>
        /// 测试取消并行
        /// </summary>
        /// <param name="matrices"></param>
        /// <param name="degrees"></param>
        /// <param name="token"></param>
        void RotateMatricesWithCancellationToke(IEnumerable<Matrix> matrices, float degrees, CancellationToken token)
        {
            Parallel.ForEach<Matrix>(matrices, new ParallelOptions() { CancellationToken = token }, body: x =>
            {
                x.Rotate(degrees);
            });
        }

        /// <summary>
        /// 测试并行共享变量
        /// </summary>
        /// <param name="matrices"></param>
        /// <param name="drgess"></param>
        /// <returns></returns>
        int InvertMatrices(IEnumerable<Matrix> matrices,float drgess)
        {
            object objLocked = new object();
            int invertMatricesNumber = 0;
            Parallel.ForEach<Matrix>(matrices, matrice =>
            {
                if (matrice.IsInvertible)
                {
                    matrice.Rotate(drgess);
                }
                else
                {
                    lock (objLocked)
                    {
                        invertMatricesNumber++;
                    }
                }
            });
            return invertMatricesNumber;
        }
    }
}
