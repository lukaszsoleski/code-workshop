using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;
namespace csharp_and_dotnet4_6
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        private static bool isDone = false;
        static void Main(string[] args)
        {
            Console.WriteLine("Thread : " + Thread.CurrentThread.ManagedThreadId);
            
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult asyncResult = b.BeginInvoke(10, 10,new AsyncCallback(AddComplete),"Main() added these numbers.");


            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working...");
            }


         


            Console.Read();

              
        }
        static int Add(int x , int y )
        {
            Console.WriteLine("Add method  {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
      
            return x + y; 
        }
        static void AddComplete(IAsyncResult asyncResult)
        {
            Console.WriteLine("Add Complete invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            AsyncResult ar = (AsyncResult)asyncResult;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;// Returns reference to delegete object on which the asynchronous call was invoked
            Console.WriteLine("Result : {0} and message: {1} " , b.EndInvoke(asyncResult), (string)asyncResult.AsyncState);
            isDone = true;
        }
    }
}
