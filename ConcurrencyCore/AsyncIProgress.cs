using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class AsyncIProgress
    {
        static async Task MyProgessAsync(IProgress<int> progess = null)
        {
            int percentProgess = 0;
            await Task.Run(async () =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
                    percentProgess++;
                    if (progess != null)
                        progess.Report(percentProgess);
                }
            });
        }

        public static async Task CallMyProgessAsync()
        {
            var progess = new Progress<int>();
            progess.ProgressChanged += Progess_ProgressChanged;
            await MyProgessAsync(progess);
        }

        private static void Progess_ProgressChanged(object sender, int e)
        {
            Console.WriteLine(e);
        }
    }
}
