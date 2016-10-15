using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestProjectApp
{
    [TestClass]
    public class AsyncTestUnit
    {
        [TestMethod]
        public async Task TestTaskDelayAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
