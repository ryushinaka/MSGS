using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MiniScript.MSGS.Scripts;

namespace MiniScript.MSGS.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(menuName = "MiniScript/New Script")]
    public class MiniScriptScriptAsset : MiniScriptScriptableObject
    {
        [HideInInspector]
        public MiniScriptScriptableObjectType ScriptableObjectType;

        [Tooltip("These are scripts that are added before this scripts contents are compiled." +
            "\n" + "This is like a #include from C++")]
        public List<MiniScriptScriptAsset> PrependedScripts = new List<MiniScriptScriptAsset>();

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

        public Interpreter GetInterpreter()
        {
            Interpreter intp = new Interpreter(GetScriptFull());
            intp.Compile();
            ScriptModuleConfiguration.AssignGlobals(ref intp);

            return intp;
        }

        #region ScriptableObject inherited methods
        [Button]
        public void ForceValidate()
        {
            Interpreter intp = new Interpreter(); //our script interpreter instance
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            #region modules
            intp.SetGlobalValue("Audio", ScriptModuleConfiguration.Audio);
            intp.SetGlobalValue("Data", ScriptModuleConfiguration.Data);
            intp.SetGlobalValue("Database", ScriptModuleConfiguration.Database);
            intp.SetGlobalValue("Host", ScriptModuleConfiguration.Host);
            intp.SetGlobalValue("Json", ScriptModuleConfiguration.Json);
            intp.SetGlobalValue("UI", ScriptModuleConfiguration.UI);
            intp.SetGlobalValue("Network", ScriptModuleConfiguration.Network);
            intp.SetGlobalValue("Schedule", ScriptModuleConfiguration.Schedule);
            intp.SetGlobalValue("Time", ScriptModuleConfiguration.Time);
            intp.SetGlobalValue("Xml", ScriptModuleConfiguration.Xml);
            intp.SetGlobalValue("Zip", ScriptModuleConfiguration.Zip);
            intp.SetGlobalValue("Unity", ScriptModuleConfiguration.Unity);
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
            //Interpreter intp = new Interpreter();
            //System.Text.StringBuilder str = new System.Text.StringBuilder();
            //ScriptModuleConfiguration.AssignGlobals(ref intp);
            
            //if (PrependedScripts != null && PrependedScripts.Count > 0)
            //{
            //    foreach (MiniScriptScriptAsset mssa in PrependedScripts) { str.AppendLine(mssa.GetScriptFull()); }
            //}

            //str.AppendLine(ScriptContent);

            //var parser = new Parser();
            //string result = string.Empty;
            //try
            //{
            //    //we just need the parser for this validation
            //    parser.Parse(str.ToString(), false);
            //    //its possible there is an incomplete statement in the script, so we check for that
            //    if (parser.NeedMoreInput()) { result += this.name + " script is incomplete."; }
            //}
            //catch (MiniScriptException mse)
            //{
            //    if (mse.location != null)
            //    {
            //        if (result.Length > 0) { result += "\n Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
            //        else { result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message; }
            //    }
            //    else
            //    {
            //        //result += "Script Error @Line " + mse.location.lineNum + " " + mse.Message;
            //        //this needs to be handled but UnityEditor calls this inconsistently/incorrectly
            //    }
            //}
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
