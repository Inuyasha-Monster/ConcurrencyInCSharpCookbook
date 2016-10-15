using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class AsyncException
    {
        static async Task ThrowNotSopportExceptionAsync()
        {
            await Task.Run(() =>
            {
                throw new NotSupportedException("我是不支持的异常");
            });
        }

        public static async Task TryTestExceptionAsync()
        {
            try
            {
                await ThrowNotSopportExceptionAsync();
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task ThrowExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("Test");
        }

        public static async Task TestThrowExceptionAsync()
        {
            var task = ThrowExceptionAsync2();
            try
            {
                await task;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Task ThrowExceptionAsync2()
        {
            return Task.Run(() =>
            {
                throw new InvalidOperationException("Test2");
            });
        }
    }
}
