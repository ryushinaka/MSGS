using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace MiniScript.MSGS.Editor
{
    //grep reference manual for arguments and features
    //https://www.gnu.org/software/grep/manual/html_node/index.html

    public class TerminalWindowScript : OdinEditorWindow
    {
        //public override bool ShowSerializedDataMode => false;

        // This string will hold the command input from the user.
        [Title("Command Input", horizontalLine: true)]
        public string command;

        // This button will execute the command when clicked.
        [Button("Execute Command")]
        public void ExecuteCommand()
        {   
            if (command.StartsWith("grep"))
            {
                var args = command.Split(" ");
                if (args[2].StartsWith("*."))
                {
                    //pattern to match is in args[1], args[2] is the files to match on/with
                    if (args[2].Substring(2).Contains("cs"))
                    {
                        Debug.Log("grep'ing on all .CS files for the pattern '" + args[1] + "'");
                        // Find all assets of type MonoScript (which includes .cs files)
                        string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { "Assets" });

                        List<System.Tuple<string, string, int>> results = new List<System.Tuple<string, string, int>>();
                        foreach (string guid in guids) {
                            string path = AssetDatabase.GUIDToAssetPath(guid);

                            if (System.IO.Path.GetExtension(path) == ".cs") {   
                                string fileContents = System.IO.File.ReadAllText(path);
                                var arr = fileContents.Split("\r\n");
                                
                                for(int i = 0; i < arr.Length; i++) {
                                    if(arr[i].ToLower().Contains(args[1].ToLower())) {
                                        results.Add(new System.Tuple<string, string, int>(
                                            System.IO.Path.GetFileName(path),
                                            path,
                                            i+1
                                            ));
                                    }
                                }
                            }
                        }
                        foreach(System.Tuple<string, string, int> t in results)
                        {                               
                            Debug.Log("Match found in " + GetEmbeddedLink(t.Item2,t.Item3));
                        }
                    }
                }
                else
                {
                    //??
                }
            }
        }

        // Adds a menu item to open the window.
        [MenuItem("Tools/Terminal Window")]
        private static void OpenWindow()
        {
            // Opens the window with the title "Command Editor".
            var win = GetWindow<TerminalWindowScript>("Terminal Window");
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            // Check for key events.
            Event e = Event.current;
            if (e.type == EventType.KeyDown &&
                (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
            {
                ExecuteCommand();
                e.Use(); // Prevent further handling of this event.
            }
        }

        string GetEmbeddedLink(string f, int i)
        {
            //Unity requires that embedded links use a specific text format
            //example: Assets/Folder/FileName.cs(linenumber)
            //"<a href=\"" + link + "\">" + text + "</a>";
            return "<a href=\"" + f + "\">" + f + "(" + i + ")</a>";

        }
    }
}

