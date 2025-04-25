using System.Threading;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{  
    public static class UnityCachedValues
    {
        public static int frameCount {
            get { return System.Threading.Interlocked.CompareExchange(ref _frameCount, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _frameCount, value); }
            
        }
        static volatile int _frameCount = 0;

        static double _realtimesincestartup;
        public static double realTimeSinceStartup {
            get { return System.Threading.Interlocked.CompareExchange(ref _realtimesincestartup, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _realtimesincestartup, value); }
        }

        static double _timescale;
        public static double timeScale
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _timescale, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _timescale, value); }
        }

        static double _maximumDeltaTime;
        public static double maximumDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _maximumDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _maximumDeltaTime, value); }
        }

        static double _fixedTime;
        public static double fixedTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _fixedTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _fixedTime, value); }
        }

        static double _deltaTime;
        public static double deltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _deltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _deltaTime, value); }
        }

        static double _fixedDeltaTime;
        public static double fixedDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _fixedDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _fixedDeltaTime, value); }
        }

        static double _unscaledDeltaTime;
        public static double unscaledDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _unscaledDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _unscaledDeltaTime, value); }
        }

        static double _smoothDeltaTime;
        public static double smoothDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _smoothDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _smoothDeltaTime, value); }
        }

        static double _timeSinceLevelLoad;
        public static double timeSinceLevelLoad
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _timeSinceLevelLoad, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _timeSinceLevelLoad, value); }
        }

        static double _unscaledTime;
        public static double unscaledTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _unscaledTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _unscaledTime, value); }
        }

        static double _time;
        public static double time
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _time, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _time, value); }
        }

        static double _fixedUnscaledTime;
        public static double fixedUnscaledTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _fixedUnscaledTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _fixedUnscaledTime, value); }
        }

        static double _maximumParticleDeltaTime;
        public static double maximumParticleDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _maximumParticleDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _maximumParticleDeltaTime, value); }
        }

        static double _fixedUnscaledDeltaTime;
        public static double fixedUnscaledDeltaTime
        {
            get { return System.Threading.Interlocked.CompareExchange(ref _fixedUnscaledDeltaTime, 0, 0); }
            set { System.Threading.Interlocked.Exchange(ref _fixedUnscaledDeltaTime, value); }
        }

        static Resolution _resolution;
        public static Resolution Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public static bool FullScreen
        {
            get; set;
        }

        public static FullScreenMode FullScreenMode
        {
            get;
            set;
        }

        public static int height
        {
            get;
            set;
        }

        public static int width
        {
            get;
            set;
        }

        public static ScreenOrientation orientation
        {
            get;
            set;
        }

        public static float brightness
        {
            get;
            set;
        }
    }

}
