using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCore
{
    public class AsyncWhenAll
    {
        private static HttpClient httpClient = new HttpClient();

        public static async Task TestWhenAllAsync()
        {
            var delay1 = Task.Delay(TimeSpan.FromSeconds(1));
            var delay2 = Task.Delay(TimeSpan.FromSeconds(2));
            var delay3 = Task.Delay(TimeSpan.FromSeconds(3));
            await Task.WhenAll(delay1, delay2, delay3);
        }

        public static async Task<int[]> TestWhenAllResultIntArrayAsync()
        {
            Task<int> task1 = Task.FromResult<int>(1);
            Task<int> task2 = Task.FromResult<int>(2);
            Task<int> task3 = Task.FromResult<int>(3);
            return await Task.WhenAll<int>(task1, task2, task3);
        }

        public static async Task<string[]> DownloadAllUrlsHtmlAsync(IEnumerable<string> urls)
        {
            var downloadTasks = urls.Select(x => httpClient.GetStringAsync(x));
            return await Task.WhenAll(downloadTasks);
        }
    }
}
