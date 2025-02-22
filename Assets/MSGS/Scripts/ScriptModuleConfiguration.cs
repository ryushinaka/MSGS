using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Scripts
{
    /// <summary>
    /// Flags for which Modules a Script Interpreter should be preloaded with
    /// </summary>
    public static class ScriptModuleConfiguration
    {
        public static ValMap Audio = MiniScript.MSGS.Audio.AudioIntrinsics.Get();
        public static ValMap Data = MiniScript.MSGS.Data.DataIntrinsics.Get();
        public static ValMap Database = MiniScript.MSGS.Database.DatabaseModule.Get();
        public static ValMap Host = MiniScript.MSGS.Host.HostModule.Get();
        public static ValMap Json = MiniScript.MSGS.Json.JsonModule.Get();
        public static ValMap UI = MiniScript.MSGS.MUUI.MUUIIntrinsics.Get();
        public static ValMap Network = MiniScript.MSGS.Network.NetworkModule.Get();
        public static ValMap Schedule = MiniScript.MSGS.Schedule.ScheduleManager.Get();
        public static ValMap Time = MiniScript.MSGS.Time.TimeKeeper.Get();
        public static ValMap Xml = MiniScript.MSGS.XML.XmlModule.Get();
        public static ValMap Zip = MiniScript.MSGS.Zip.ZipModule.Get();
        public static ValMap Unity = MiniScript.MSGS.Unity.UnityModule.Get();
    }
}

