using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ContentResult TestAsync()
        {
            TestAspNetContextDeadLocked().Wait();
            return Content("ok");
        }

        async Task TestAspNetContextDeadLocked()
        {
            // 死锁产生同理，asp.net上下文被相互竞争
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
        }

        public async Task<ContentResult> TestAsync2()
        {
            await TestAspNetContextDeadLocked2();
            return Content("ok");
        }

        async Task TestAspNetContextDeadLocked2()
        {
            // 死锁产生同理，asp.net上下文被相互竞争
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}