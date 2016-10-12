using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestDeadLocked_Click(object sender, EventArgs e)
        {
            DeadLockTest();
        }

        void DeadLockTest()
        {
            Task task = WaitAsync();
            MessageBox.Show("I am strat !" + Thread.CurrentThread.ManagedThreadId);
            // 阻塞了UI线程
            task.Wait();
        }

        private async Task WaitAsync()
        {
            MessageBox.Show("I am Coming !" + Thread.CurrentThread.ManagedThreadId);// 9
            //await Task.Delay(TimeSpan.FromSeconds(3));
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            // 下面的同步块想获取UI线程，但是由于上面的UI线程被阻塞，所以拿不到，又因为 DeadLockTest 方法等待 WaitAsync 完成，但是 WaitAsync 又完成不了，形成了死锁
            MessageBox.Show("I am Comed !" + Thread.CurrentThread.ManagedThreadId);// 11
        }

        private async Task WaitAsync2()
        {
            MessageBox.Show("I am Coming !" + Thread.CurrentThread.ManagedThreadId);// 9
            await Task.Delay(TimeSpan.FromSeconds(3));
            //await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            // 下面的同步块想获取UI线程，但是由于上面的UI线程被阻塞，所以拿不到，又因为 DeadLockTest 方法等待 WaitAsync 完成，但是 WaitAsync 又完成不了，形成了死锁
            MessageBox.Show("I am Comed !" + Thread.CurrentThread.ManagedThreadId);// 9
        }

        private async void btnTest2_Click(object sender, EventArgs e)
        {
            await WaitAsync2();
        }
    }
}
