using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MiniScript.MSGS.Audio;
using MiniScript.MSGS.Data;
using MiniScript.MSGS.Time;
using MiniScript.MSGS.Host;
using MiniScript.MSGS.Json;
using MiniScript.MSGS.Schedule;
using MiniScript.MSGS.MUUI;
using MiniScript.MSGS.XML;
using MiniScript.MSGS.Zip;
using MiniScript.MSGS.Network;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.ScriptableObjects
{
    [CreateAssetMenu(menuName = "MiniScript/New Script")]
    public class MiniScriptScriptAsset : MiniScriptScriptableObject
    {
        [HideInInspector]
        public MiniScriptScriptableObjectType ScriptableObjectType;

        [Tooltip("These are scripts that are added before this scripts contents are compiled." +
            "\n" + "This is like a #include from C++")]
        public List<MiniScriptScriptAsset> PrependedScripts = new List<MiniScriptScriptAsset>();

        #region Modules List
        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to Archive module?")]
        public bool Archive;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to Audio module?")]
        public bool Audio;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to the value store in-memory module?")]
        public bool Data;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to the database (sqlite) module?")]
        public bool Database;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to the host (Unity3D) module?")]
        public bool Host;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to the Json module?")]
        public bool Json;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to UI module?")]
        public bool MUUI;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to the IP network module?")]
        public bool Network;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to script scheduling module?")]
        public bool Schedule;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to Time/Clock module?")]
        public bool Time;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to XML module?")]
        public bool XML;

        [FoldoutGroup("Modules")]
        [Tooltip("Does this script require access to Zip module?")]
        public bool Zip;
        #endregion

        [ShowInInspector, TextArea(5, 150)]
        public string ScriptContent;

        public string GetScriptFull()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            foreach (MiniScriptScriptAsset mssa in PrependedScripts)
            {
                str.AppendLine(mssa.GetScriptFull());
            }

            str.AppendLine(ScriptContent);

            return str.ToString();
        }

        #region ScriptableObject inherited methods
        [Button]
        public void ForceValidate()
        {
            Interpreter intp = new Interpreter(); //our script interpreter instance
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            #region modules
            //if (Archive) { intp.SetGlobalValue("archive", ArchiveHandler.Get()); }
            if (Audio) { intp.SetGlobalValue("audio", AudioIntrinsics.Get()); }
            if (Data) { intp.SetGlobalValue("data", DataIntrinsics.Get()); }
            if (Database) { intp.SetGlobalValue("db", null); }
            if (Host) { intp.SetGlobalValue("host", HostModule.Get()); }
            if (Json) { intp.SetGlobalValue("json", JsonModule.Get()); }
            if (Network) { intp.SetGlobalValue("network", NetworkModule.Get()); }
            if (MUUI) { intp.SetGlobalValue("ui", MUUIIntrinsics.Get()); }
            if (Schedule) { intp.SetGlobalValue("schedule", ScheduleManager.Get()); }
            if (Time) { intp.SetGlobalValue("time", TimeKeeper.Get()); }
            if (XML) { intp.SetGlobalValue("xml", XmlModule.Get()); }
            if (Zip) { intp.SetGlobalValue("zip", ZipModule.Get()); }
            #endregion

            foreach (MiniScriptScriptAsset mssa in PrependedScripts)
            {
                str.AppendLine(mssa.GetScriptFull());
            }

            str.AppendLine(ScriptContent);

            var parser = new Parser();
            string result = string.Empty;
            try
            {
                //we just need the parser for this validation
                parser.Parse(str.ToString(), false);
                //its possible there is an incomplete statement in the script, so we check for that
                if (parser.NeedMoreInput()) { result += this.name + " script is incomplete."; }
            }
            catch (MiniScriptException mse)
            {
                if (mse.location != null)
                {
                    if (result.Length > 0) { result += "\n Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
                    else { result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
                }
                else
                {
                    result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message;
                }
            }

            if (result.Length > 0) { Debug.Log("OnValidate(" + this.name + "): " + result); }
        }

        /// <summary>
        /// This happens whenever the properties of the ScriptableObject have been modified
        /// </summary>
        private void OnValidate()
        {
            //Debug.Log("SO validate start: " + this.name);
            //validate the contents of the Script Asset
            Interpreter intp = new Interpreter();
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            #region modules
            //if (Archive) { intp.SetGlobalValue("archive", ArchiveHandler.Get()); }
            if (Audio) { intp.SetGlobalValue("audio", AudioIntrinsics.Get()); }
            if (Data) { intp.SetGlobalValue("data", DataIntrinsics.Get()); }
            if (Database) { intp.SetGlobalValue("db", null); }
            if (Host) { intp.SetGlobalValue("host", HostModule.Get()); }
            if (Json) { intp.SetGlobalValue("json", JsonModule.Get()); }
            if (Network) { intp.SetGlobalValue("network", NetworkModule.Get()); }
            if (MUUI) { intp.SetGlobalValue("ui", MUUIIntrinsics.Get()); }
            if (Schedule) { intp.SetGlobalValue("schedule", ScheduleManager.Get()); }
            if (Time) { intp.SetGlobalValue("time", TimeKeeper.Get()); }
            if (XML) { intp.SetGlobalValue("xml", XmlModule.Get()); }
            if (Zip) { intp.SetGlobalValue("zip", ZipModule.Get()); }
            #endregion

            foreach (MiniScriptScriptAsset mssa in PrependedScripts) { str.AppendLine(mssa.GetScriptFull()); }

            str.AppendLine(ScriptContent);

            var parser = new Parser();
            string result = string.Empty;
            try
            {
                //we just need the parser for this validation
                parser.Parse(str.ToString(), false);
                //its possible there is an incomplete statement in the script, so we check for that
                if (parser.NeedMoreInput()) { result += this.name + " script is incomplete."; }
            }
            catch (MiniScriptException mse)
            {
                if (mse.location != null)
                {
                    if (result.Length > 0) { result += "\n Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
                    else { result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
                }
                else
                {
                    result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message;
                }
            }
#if UNITY_EDITOR
            //if(result.Length > 0) { Debug.Log("OnValidate(" + this.name + "): " + result); }
#endif
        }

        private void Reset()
        {
            //Debug.Log("SO Reset");
        }

        private void OnDestroy()
        {
            //Debug.Log("SO Destroyed");    
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
        #endregion

        public MiniScriptScriptAsset()
        {
            this.ScriptableObjectType = MiniScriptScriptableObjectType.EmbeddedScript;
        }
    }

    public class MiniScriptScriptAssetDrawer //: OdinValueDrawer<MiniScriptScriptAsset>
    {
        //InspectorProperty content;

        //protected override void Initialize()
        //{
        //    base.Initialize();
        //    content = Property.Children["ScriptContent"];
        //}
        //protected override void DrawPropertyLayout(GUIContent label)
        //{
        //    base.DrawPropertyLayout(label);

        //    content.ValueEntry.WeakSmartValue = EditorGUILayout.TextField((string)content.ValueEntry.WeakSmartValue);
        //}
    }

    public static class MiniScriptScriptAssetExtensions
    {
        public static void WriteToFile(this MiniScriptScriptAsset asset, string path)
        {
            var wrt = System.IO.File.CreateText(path);
            wrt.Write(asset.ScriptContent);
            wrt.Flush();
            wrt.Close();
        }

        public static MiniScriptScriptAsset ReadFromFile(this MiniScriptScriptAsset asset, string path)
        {
            MiniScriptScriptAsset blah = new MiniScriptScriptAsset();
            var rdr = System.IO.File.OpenText(path);
            blah.ScriptContent = rdr.ReadToEnd();
            rdr.Close(); rdr = null;

            return blah;
        }
    }

}
