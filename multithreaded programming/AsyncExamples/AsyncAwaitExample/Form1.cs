using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
namespace AsyncAwaitExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine("Ctor:  " + Thread.CurrentThread.ManagedThreadId);
        }

        private async void btnCallMethod_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Button click:  " + Thread.CurrentThread.ManagedThreadId);
            //this will be executed first
            await MethodReturningVoid();
            // and then this after above line
            this.Text = await DoWorkAsync();
        }
        private Task<string> DoWorkAsync()
        {
            //the following line blocks main thread
            //Thread.Sleep(5000);

            return Task.Run(() =>
            {
                Debug.WriteLine("DoWork() return Task.Run( () => { here }. Thread: " + Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(2000);
                return "Done with work!";
            });
        }
        private async Task MethodReturningVoid()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);

            });

        }
    }
}
