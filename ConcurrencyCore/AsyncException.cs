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
    }
}
