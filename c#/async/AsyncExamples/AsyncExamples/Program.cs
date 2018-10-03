using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace AsyncExamples
{
    public delegate int BinaryOp(int x, int y);

    public interface MyAsyncTest {
        void Start();
    }

    class Program
    {
        static List<MyAsyncTest> myAsyncTests = new List<MyAsyncTest>
        {
        //    new SyncDelegateReview(),
        //    new IAsyncResultReview(),
        //    new AsyncIsCompleted(),
        //    new AsyncWaitHandleWaitOneExample(),
        //    new AsyncCallbackExample(),
        //    new ThreadStartExample(),
         //  new ParameterizedThreadStartExample(),
           new AsyncAwaitExample()

        };
        static void Main(string[] args)
        {
            foreach(var myasync in myAsyncTests)
            {
                myasync.Start();
            }
            Console.WriteLine("Next Enter will close the console.");
            Console.ReadLine();
        }
    }
    #region by using delegates
    #region SyncDelegateReview
    class SyncDelegateReview : MyAsyncTest
    {

        public void Start()
        {
            Console.WriteLine("**** Sync Delegate Review ****");

            // Print the ID of the current thread
            Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            int answer = b(10, 10);// same as b.invoke(10,10); 


            // These lines will not be executed until the add method ends
            Console.WriteLine("Doing more work in Main()");
            Console.WriteLine("10 + 10 = {0}", answer);
            Console.Read();
        }

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);

            return x + y;
        }
    }
    #endregion
    #region IAsyncResultReview
    class IAsyncResultReview : MyAsyncTest
        {

            public void Start()
            {
                Console.WriteLine("**** IAsyncResult ****");

             // Print the ID of the current thread
                Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);

                BinaryOp b = new BinaryOp(Add);
                IAsyncResult asyncResult = b.BeginInvoke(10, 10,null,null);

                //This will show up right away
                Console.WriteLine("Doing more work in Main()");
                // Stop the current thread and wait for the add() method
                int answer = b.EndInvoke(asyncResult);
                
                Console.WriteLine("10 + 10 = {0}", answer);
                Console.ReadLine();
            }
                
            static int Add(int x, int y)
            {
                Console.WriteLine("Add() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(5000);

                return x + y;
            }
        }
    #endregion

    #region AsyncIsCompleted
    class AsyncIsCompleted : MyAsyncTest
    {

        public void Start()
        {
            Console.WriteLine("**** Async Is Completed ****");

            // Print the ID of the current thread
            Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult asyncResult = b.BeginInvoke(10, 10, null, null);


            while (!asyncResult.IsCompleted)
            {
                Console.WriteLine("Doing work in Main()");
                Thread.Sleep(500);
            }

            int answer = b.EndInvoke(asyncResult);

            Console.WriteLine("10 + 10 = {0}", answer);
            Console.ReadLine();
        }

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);

            return x + y;
        }
    }

    #endregion

    #region AsyncWaitHandleWaitOne
    class AsyncWaitHandleWaitOneExample : MyAsyncTest
    {

        public void Start()
        {
            Console.WriteLine("**** Async Wait Handle - Wait One Example ****");

            // Print the ID of the current thread
            Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult asyncResult = b.BeginInvoke(10, 10, null, null);


            while (!asyncResult.AsyncWaitHandle.WaitOne(500,true))
            {
                Console.WriteLine("Doing work in Main()");
            //    Thread.Sleep(500);
            }

            int answer = b.EndInvoke(asyncResult);

            Console.WriteLine("10 + 10 = {0}", answer);
            Console.ReadLine();
        }

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);

            return x + y;
        }
    }
    #endregion

    #region AsyncCallbackExample
    class AsyncCallbackExample : MyAsyncTest
    {
        //Variable is shared between two threads
        private static bool isDone = false;

        public void Start()
        {
            Console.WriteLine("**** Async Callbac ****");

            // Print the ID of the current thread
            Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);
            
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult asyncResult = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete) ,"Main() thanks you for adding these numbers :)");


           // while (!asyncResult.AsyncWaitHandle.WaitOne(500, true))
           while(!isDone)
            {
                Console.WriteLine("Doing work in Main()");
                  Thread.Sleep(500);
            }

            Console.ReadLine();
        }
        static void AddComplete(IAsyncResult asyncResult)
        {
            Console.WriteLine("AddComplete() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Your addition is complete");

            AsyncResult ar = (AsyncResult)asyncResult;
            string message = (string)ar.AsyncState;

            // Gets the instance of the delegate in main thread
            BinaryOp binaryOp = (BinaryOp)ar.AsyncDelegate;

            Console.WriteLine("10 + 10 = {0}",binaryOp.EndInvoke(ar));
            Console.WriteLine(message);

            isDone = true;
        }
        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);

            return x + y;
        }
    }
    #endregion
    #endregion

    #region by using threads

    #region ThreadStart
    class ThreadStartExample : MyAsyncTest
    {
       

        public void Start()
        {
            Console.WriteLine("**** Thread Start ****");

            // Print the ID of the current thread
            Console.WriteLine("Invoked on thread : {0} ", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Do you want 1 or 2 threads?");
            string threadCount = Console.ReadLine();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary Thread";

            Console.WriteLine("-> {0} is executing Start()",Thread.CurrentThread.Name);

            Printer printer = new Printer();

            switch (threadCount)
            {
                case "2":
                    Thread backgroundThread = new Thread(new ThreadStart(printer.PrintNumbers));
                    backgroundThread.Name = "Secondary Thread";
                    backgroundThread.Start();

                    break;
                case "1":
                    printer.PrintNumbers();
                    break;
                default:
                    Console.WriteLine("I don't know what you want... you get 1 thread");
                    goto case "1"; 

            }

            MessageBox.Show("I'm busy !", "Work on main thread... ");
            Console.ReadLine(); 

            Console.ReadLine();
        }

        class Printer
        {
            public void PrintNumbers()
            {
                Console.WriteLine("{1} invoked on thread : {0} is executing PrintNumbers() ", Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.Name);

                Console.WriteLine("Your numbers");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("{0}, ", i);
                    Thread.Sleep(2000);

                }
                Console.WriteLine();
            }
        }
    }
    #endregion

    #region Parameterized Thread Start
    class ParameterizedThreadStartExample : MyAsyncTest
    {


        public void Start()
        {
            Console.WriteLine("**** Parameterized thread start ****");

            // Print the ID of the current thread
            Console.WriteLine("Id of thread start : {0} ", Thread.CurrentThread.ManagedThreadId);

            // Create instance of new object 
            AddParams ap = new AddParams(8, 10);

            // Create new instance of Thread and initialize it with the ParameterizedThreadStart delegate
            //that points to the Add method
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            //Send the AddParams object to crated thread. 
            t.Start(ap);


            
            Console.ReadLine();

            Console.ReadLine();
        }

       public static void Add(object data)
        {
            if(data is AddParams)
            {
                Console.WriteLine();

                Console.WriteLine("Id of Add() : {0}", Thread.CurrentThread.ManagedThreadId);
                AddParams ap = (AddParams)data;

                Console.WriteLine("{0} + {1} is {2}", ap.a, ap.b, ap.a + ap.b);

            }
        }

        class AddParams
        {
            public int a, b;
            public AddParams(int num1, int num2)
            {
                a = num1;
                b = num2;
            }
        }
    }

    //using async await: 
    class AsyncAwaitExample : MyAsyncTest
    {


        public void Start()
        {
            Console.WriteLine("**** Async Await ****");
            Console.WriteLine("Id of Main() : {0}", Thread.CurrentThread.ManagedThreadId);

            AddAsync();
            Console.WriteLine("Id of Mani() After Async Call : {0}", Thread.CurrentThread.ManagedThreadId);



            Console.ReadLine();
        }

        public static async Task AddAsync()
        {
            Console.WriteLine("**** AddAsync() ****");
            Console.WriteLine("Id of AddAsync() : {0}", Thread.CurrentThread.ManagedThreadId);
            AddParams add = new AddParams(10, 10);
            await Sum(add);
            
        }
        static async Task Sum(object data)
        {
            Console.WriteLine("Id of Sum : {0}", Thread.CurrentThread.ManagedThreadId);

            await Task.Run(() => {
                if (data is AddParams)
                {
                    Console.WriteLine("Id of Sum => TaskRun() : {0}", Thread.CurrentThread.ManagedThreadId);
                    AddParams add = (AddParams)data;
                    Console.WriteLine("{0} + {1} = {2}", add.a, add.b, add.a + add.b);

                }

            });
        }
        class AddParams
        {
            public int a, b;
            public AddParams(int num1, int num2)
            {
                a = num1;
                b = num2;
            }
        }
    }
    #endregion



    #endregion
}
