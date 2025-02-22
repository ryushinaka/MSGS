using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Data;

namespace MiniScript.MSGS.Time
{
    public static class TimeKeeper
    {
        static List<System.Tuple<string, double, double>> intervals;
        static List<double> clockvalues;
        static object synclock;
        static ValMap timeIntrinsics;
#pragma warning disable CS0414
        static volatile bool stopped, running, paused, canRun, testMode;
#pragma warning restore CS0414
        static System.DateTime lastchecked; //avoids allocation in loop
        static System.TimeSpan lasteval;   //avoids allocation in loop
        static int i = 0;                  //avoids allocation in loop

        public static void Initialize()
        {
            synclock = new object();
            stopped = true; running = false; paused = true; testMode = false;
            timeIntrinsics = new ValMap();
            intervals = new List<System.Tuple<string, double, double>>();
            clockvalues = new List<double>();

            var a = Intrinsic.Create("");
            #region Now
            a.code = (context, partialResult) =>
            {
                System.DateTime dt = System.DateTime.Now;
                ValList vl = new ValList();
                vl.values.Add(new ValNumber(dt.Millisecond));
                vl.values.Add(new ValNumber(dt.Second));
                vl.values.Add(new ValNumber(dt.Minute));
                vl.values.Add(new ValNumber(dt.Hour));
                vl.values.Add(new ValNumber(dt.Day));
                vl.values.Add(new ValNumber(dt.Month));
                vl.values.Add(new ValNumber(dt.Year));

                return new Intrinsic.Result(vl, true);
            };

            timeIntrinsics.map.Add(new ValString("Now"), a.GetFunc());
            #endregion
        }

        static void Start() 
        {
            stopped = false;
            canRun = false;
            running = true;
            paused = false;
        }

        static void Stop() 
        {
            stopped = true;
            canRun = true;
            running = false;
            paused = false;
        }

        static void Pause() 
        {

        }

        static void Resume() 
        {

        }

        static void TimeChecker(object o)
        {
            if (intervals.Count > 0 && running)
            {
                if (!paused)
                {
                    lock (synclock)
                    {
                        lasteval = System.DateTime.Now - lastchecked;

                        for (i = 0; i < intervals.Count; i++)
                        {

                        }

                        //is the current time value more than or equal to the interval time of the first interval?
                        if (lasteval.TotalMilliseconds >= intervals[0].Item2)
                        {
                            //do we have 2 intervals on the clock? then increment the 2nd interval and reset the first to 0
                            if (clockvalues[0] + 1 > intervals[0].Item3 && intervals[0].Item3 != -1 && intervals[0].Item3 > 0
                                && intervals.Count >= 2)
                            {
                                clockvalues[0] = 0;
                                
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(1);
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(TimeChecker));
            }
        }

        public static ValMap Get()
        {
            if (timeIntrinsics == null) { Initialize(); }
            return timeIntrinsics;
        }
    }
}

