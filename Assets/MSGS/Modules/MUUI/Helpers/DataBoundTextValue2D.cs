using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MiniScript.MSGS.MUUI;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(MUUI.TwoDimensional.MUUIText))]
    public class DataBoundTextValue2D : MonoBehaviour
    {
        [Tooltip("The Data reference to lookup (eg. globals[\"Data\"].???)"), SerializeField]
        public string link;
        [Tooltip("1 = update on every frame, 2 = every other frame, 3 = every 3rd frame, etc"), Min(1), MaxValue(60)]
        public int updateFrequency = 1;

        public int lastUpdate = 0;
        public bool isUpdating = false;
        TMPro.TextMeshProUGUI label;

        public string GetLink
        {
            get { return link; }
            private set { }
        }
        public void SetLink(ref string newlink)
        {
            link = newlink;
        }

        void Start()
        {
            label = GetComponent<TMPro.TextMeshProUGUI>();
        }

        void Update()
        {
            //there was a discussion previously in #Embedders for how to get function results from outside the Interpreter
            //that will likely be necessary for this functionality, as using globals["??"]["???"] requires a fixed reference
            //that while having a ScriptableObject or MiniScriptSingleton reference the datamodel.ms is possible, is it 
            //a good option?  at this time this does not appear to be a clear cut issue but there is something simple/obvious
            //about this that the final solution should be easy to implement.

            //so for the purposes of testing, the current format for 'link' string literals is just a miniscript statement
            //that evaluates using the globals["Data"] namespace and context.
            //globals["Data"].GetInstances("myType")[index][property] as a simple example
            //once implemented, the Database query format can also be used
            //globals["Database"].Select("?").Where("?","??").Where("etc","etc").End()

            if (link.StartsWith("globals[\"Database\"]"))
            {
                lastUpdate = 0;
            }
            else if (link.StartsWith("globals[\"Data\"]"))
            {
                var ones = ScriptableObject.CreateInstance<OneShotScript>();
                var vm = GetComponent<MUUI.TwoDimensional.MUUIText>().GetProperties(); //get the properties of the control as a valmap                
                //now we craft the script source based on what we want to achieve
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendLine("txt = " + GetLink);
                str.AppendLine("globals[\"Control\"][\"Text\"][\"Text\"] = txt");

                ones.scriptSource = str.ToString();

                ones.Compile(); //we have to compile first, before we add any global variables
                ones.AddGlobal("Control", vm); //add the Control valmap
                ones.RunSync(); //execute the script

                if (ones.hasErrors)
                {
                    string err = string.Empty;
                    err = "Qty: " + ones.err_msgs.Count + " // ";
                    foreach (string s in ones.err_msgs) { err += s + ","; }
                    Debug.Log("DTVerr: " + err);
                }
                if (ones.hasStdOutput)
                {
                    string strS = string.Empty;
                    strS = "Qty: " + ones.std_output.Count + " // ";
                    foreach (string s in ones.std_output) { strS += s + ","; }
                    Debug.Log("DTVStd: " + strS);
                }
                if (ones.needsMoreInput) { Debug.Log("needs more input"); }
            }
            else if (link.StartsWith("globals[\"Unity\"]")) {

                var ones = ScriptableObject.CreateInstance<OneShotScript>();
                var vm = GetComponent<MUUI.TwoDimensional.MUUIText>().GetProperties(); //get the properties of the control as a valmap                
                //now we craft the script source based on what we want to achieve
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendLine("txt = " + GetLink);
                str.AppendLine("globals[\"Control\"][\"Text\"][\"Text\"] = txt");

                ones.scriptSource = str.ToString();

                ones.Compile(); //we have to compile first, before we add any global variables
                ones.AddGlobal("Control", vm); //add the Control valmap
                ones.RunSync(); //execute the script

                if (ones.hasErrors)
                {
                    string err = string.Empty;
                    err = "Qty: " + ones.err_msgs.Count + " // ";
                    foreach (string s in ones.err_msgs) { err += s + ","; }
                    Debug.Log("DTVerr: " + err);
                }
                if (ones.hasStdOutput)
                {
                    string strS = string.Empty;
                    strS = "Qty: " + ones.std_output.Count + " // ";
                    foreach (string s in ones.std_output) { strS += s + ","; }
                    Debug.Log("DTVStd: " + strS);
                }
                if (ones.needsMoreInput) { Debug.Log("needs more input"); }
            }
        }

        [Button]
        void DebugProperties()
        {
            var vm = GetComponent<MUUI.TwoDimensional.MUUIText>().GetProperties(); //get the properties of the control as a valmap
            foreach (KeyValuePair<Value, Value> kv in vm.map)
            {
                Debug.Log(kv.Key.ToString() + " / " + kv.Value.ToString());
            }
        }
    }
}
