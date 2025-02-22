using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniScript.MSGS.Schedule
{
    /// <summary>
    /// Allows the scheduling of scripts based on time
    /// </summary>
    public static class ScheduleManager
    {
        static bool hasInitialized;
        static ValMap timeIntrinsics;

        public static void Initialize()
        {
            timeIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region  ScheduleScript
            a.AddParam("script", string.Empty);
            a.AddParam("interval", (double)6000); //default to 6000ms which is 1 minute
            a.AddParam("repeat", ValNumber.Truth(false)); //default to false
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };
            #endregion

            #region  OncePerMinute
            a.AddParam("script", string.Empty);            
            a.AddParam("repeat", ValNumber.Truth(false)); //default to false
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };
            #endregion

            #region  OncePerHalfHour
            a.AddParam("script", string.Empty);
            a.AddParam("repeat", ValNumber.Truth(false)); //default to false
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };
            #endregion

            #region  OncePerHour
            a.AddParam("script", string.Empty);
            a.AddParam("repeat", ValNumber.Truth(false)); //default to false
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };
            #endregion

            hasInitialized = true;
        }

        public static ValMap Get()
        {
            if(!hasInitialized) { Initialize(); }
            return timeIntrinsics;
        }
    }
}

