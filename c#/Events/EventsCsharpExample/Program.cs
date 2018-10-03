using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Events example
namespace EventsCsharpExample
{
   
        /// <summary>
    /// A class to hold the information about the event 
    /// In this case it will hold only information 
    /// availble in the clock class , but could hold 
    /// additional state information
    /// </summary>
    public class TimeInfoEventArgs : EventArgs
    {
        public int hour;
        public int minute;
        public int second;

        public TimeInfoEventArgs(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    /// <summary>
    /// The publisher: the class that other classes 
    /// will observe. This class publishes one delegate 
    /// SecondChangeHandler
    /// </summary>
    public class Clock
    {
        private int hour;
        private int minute;
        private int second;

        // the delegate the subscriers must implement
        public delegate void SecondChangeHandler(object clock,
            TimeInfoEventArgs timeInformation);
       
        // following code can cause problems:
        //an instance of the delagate
        // public SecondChangeHandler SecondChanged; 

        // u should use event keyword that restricts access to the instance of SecondChangeHandler delegate
        public event SecondChangeHandler SecondChanged;

        /*
         *  The event keyword indicates to the compiler that the delegate can be invoked only by the defining class, and that other classes can subscribe to and unsubscribe from the delegate using only the appropriate += and -= operators, respectively.
         */

        /*
        The event keyword indicates to the compiler that the delegate can be invoked only by the defining class
         Classes can no longer attempt to subscribe 
        to the event using the assignment operator (=),
        as they could previously, nor can they invoke the event
        directly, as was done in the preceding example.
        Either of these attempts will now generate a compile error
        */


        // set the clock running 
        // it will raise an event for each new second
        public void Run()
        {
            for (;;)
            {
                //sleep 100 miliseconds
                System.Threading.Thread.Sleep(100);
                // get the current time 
                System.DateTime dt = System.DateTime.Now;
                // if the second has changed 
                // notify the subscribers


                if (dt.Second != second)
                {
                    //create hte TimeInfoEventArgs object
                    // to pass to the subscriber
                    TimeInfoEventArgs timeInformation =
                        new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);
                    //if anyone has subscribed , notify them 
                    if (SecondChanged != null)
                        SecondChanged(this, timeInformation);

                }
                // update the state

                this.second = dt.Second;
                this.minute = dt.Minute;
                this.hour = dt.Hour;


            }

        }

    }
    public class DisplayClock
    {
        // given a clock, subscribe to 
        // its SecondCHangeHandler event 
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChanged += TimeHasChanged; // shortcut
        }

        // the method that implements the 
        // delegate functionality 
        private void TimeHasChanged(object clock, TimeInfoEventArgs timeInformation)
        {
            Console.WriteLine("Current Time: {0}:{1}:{2}"
                , timeInformation.hour.ToString(), timeInformation.minute.ToString(), timeInformation.second.ToString());


        }
        // a second subscriber whose job is to write to a file 
        public class LogCurrentTime
        {
            public void Subscribe(Clock theClock)
            {
                theClock.SecondChanged +=
                    new Clock.SecondChangeHandler(WriteLogEntry);
            }

            private void WriteLogEntry(object clock, TimeInfoEventArgs timeInformation)
            {
                Console.WriteLine("Logging to file: {0}:{1}:{2}"
                    , timeInformation.hour.ToString(), timeInformation.minute.ToString(), timeInformation.second.ToString());
            }

        }
        public class Tester
        {
            public void Run()
            {
                Clock theClock = new Clock();

                // create the display and tell it to 
                // subscribe to the clock just created 
                DisplayClock dc = new DisplayClock();
                dc.Subscribe(theClock);
                // create a Log object and tell it 
                // to subscribe to the clock 
                LogCurrentTime lct = new LogCurrentTime();
                lct.Subscribe(theClock);
                // get the clock started 
                theClock.Run();
            }
        }

        class Program
        {




            static void Main(string[] args)
            {
                Tester t = new Tester();
                t.Run(); 
            }
        }
    }
}
