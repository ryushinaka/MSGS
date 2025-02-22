using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using MiniScript.MSGS.ScriptableObjects;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Editor
{   
    public class MiniScriptCodeEditor : OdinEditorWindow
    {
        [MenuItem("MiniScript/Script Editor")]
        private static void OpenWindow()
        {
            GetWindow<MiniScriptCodeEditor>().Show();
        }

        [InlineEditor(Expanded = true)]
        [SerializeField] private MiniScriptScriptAsset mySO; // Holds the text content

        private Vector2 scrollPos;
        private const float lineNumberWidth = 30f;
        private string textFieldControlName = "SyntaxEditor";
        private int lastCursorIndex = -1;

        // Define syntax highlighting rules (example: keywords, numbers, and comments)
        private static readonly Regex keywords = new Regex(@"\b(public|private|void|int|string|float|bool|return|if|else|for|while|class|namespace|using)\b");
        private static readonly Regex numbers = new Regex(@"\b\d+\b");
        private static readonly Regex comments = new Regex(@"(\/\/.*)");

        private GUIStyle defaultStyle;
        private GUIStyle keywordStyle;
        private GUIStyle numberStyle;
        private GUIStyle commentStyle;

        // Open the Editor Window for a given TextData asset
        public static void ShowWindow(MiniScriptScriptAsset data)
        {
            if (data == null)
            {
                Debug.LogError("No TextData assigned!");
                return;
            }

            var window = GetWindow<MiniScriptCodeEditor>(true, "MiniScript Code Editor", true);
            window.mySO = data;
            window.minSize = new Vector2(500, 350);
            window.InitializeStyles(); // Initialize styles for syntax highlighting
        }

        private void InitializeStyles()
        {
            defaultStyle = new GUIStyle(EditorStyles.textField) { richText = true };
            keywordStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = Color.blue } };
            numberStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = Color.cyan } };
            commentStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = Color.green } };
        }

        protected override void OnGUI()
        {
            if (mySO == null)
            {
                EditorGUILayout.HelpBox("No TextData assigned!", MessageType.Error);
                if (GUILayout.Button("Close")) Close();
                return;
            }

            EditorGUILayout.LabelField("Editing: " + mySO.name, EditorStyles.boldLabel);

            // Begin Scroll View
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

            EditorGUILayout.BeginHorizontal();

            // Draw Line Numbers
            DrawLineNumbers();

            // Draw Syntax Highlighted TextArea
            DrawSyntaxTextArea();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();

            // Save & Close Buttons
            if (GUILayout.Button("Save")) SaveText();
            if (GUILayout.Button("Close")) Close();
        }

        private void DrawLineNumbers()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(lineNumberWidth));

            string[] lines = mySO.ScriptContent.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                EditorGUILayout.LabelField((i + 1).ToString(), GUILayout.Width(lineNumberWidth));
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawSyntaxTextArea()
        {
            GUI.SetNextControlName(textFieldControlName);

            EditorGUI.BeginChangeCheck();
            string newText = HighlightSyntax(mySO.ScriptContent);

            // Draw RichText Label instead of raw TextArea
            EditorGUILayout.TextArea(newText, defaultStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (EditorGUI.EndChangeCheck())
            {
                int cursorIndex = GUIUtility.keyboardControl == EditorGUIUtility.GetControlID(FocusType.Keyboard) ? GUIUtility.keyboardControl : lastCursorIndex;

                mySO.ScriptContent = newText;
                EditorUtility.SetDirty(mySO);
                Repaint();

                EditorGUI.FocusTextInControl(textFieldControlName);
                GUIUtility.keyboardControl = cursorIndex;
            }

            lastCursorIndex = GUIUtility.keyboardControl;
        }

        private string HighlightSyntax(string inputText)
        {
            // Apply regex highlighting to the input text
            inputText = keywords.Replace(inputText, "<color=#0000ff>$1</color>"); // Blue for keywords
            inputText = numbers.Replace(inputText, "<color=#00ffff>$0</color>"); // Cyan for numbers
            inputText = comments.Replace(inputText, "<color=#00ff00>$1</color>"); // Green for comments

            return inputText;
        }

        private void SaveText()
        {
            EditorUtility.SetDirty(mySO);
            AssetDatabase.SaveAssets();
        }
    }

    [CustomEditor(typeof(MiniScriptScriptAsset))]
    public class InspectorCodeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Editor"))
            {
                MiniScriptCodeEditor.ShowWindow((MiniScriptScriptAsset)target);
            }
        }
    }

}

