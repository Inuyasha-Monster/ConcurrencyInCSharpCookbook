using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class ParallelAggregate
    {
        /// <summary>
        /// 原始写法
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int num = 0;
            Parallel.ForEach<int>(source: values, body: value =>
            {
                lock (mutex)
                {
                    num = num + value;
                }
            });
            return num;
        }

        /// <summary>
        /// PLINQ写法
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static int ParallelPlinqSum(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        /// <summary>
        /// 基于PLINQ聚合写法
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static int ParallelPlinqAggregate(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate((x, y) => x + y);
        }

        /// <summary>
        /// Parallel.Invoke(params Action<T> actions)
        /// </summary>
        /// <param name="array"></param>
        static void ProcessArray(double[] array)
        {
            Parallel.Invoke(() =>
            {
                ProcessPatialArray(array, 0, array.Length / 2);
            }, () =>
            {
                ProcessPatialArray(array, array.Length / 2, array.Length);

            });
        }

        static void ProcessPatialArray(double[] array, int bengin, int end)
        {
            // 计算密集的处理过程
            Thread.Sleep(TimeSpan.FromSeconds(1));// 模拟计算过程
        }

        /// <summary>
        /// 保持顺序的并行投影
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static int[] LoopArrayToDouble(IEnumerable<int> values)
        {
            return values.AsParallel<int>().AsOrdered<int>().Select(x => x * 2).ToArray();
        }
    }
}
