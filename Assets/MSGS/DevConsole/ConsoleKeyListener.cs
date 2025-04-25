using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Unity;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.Unity.DevConsole
{
    public class ConsoleKeyListener : MonoBehaviour
    {
        public KeyCode ActivationKey;
        public GameObject Parent;
        public TMPro.TextMeshProUGUI Output;
        public TMPro.TMP_InputField input;
        public int outputCharLimit, textOutputFontSize, textInputFontSize, caretPosition;

        public Color inputColor, inputBGColor, outputColor, outputBGColor;

        public bool debugMode = false;

        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=+_{}[]\\/;:'\",.<> ";

        void Start()
        {   
            input.onValidateInput += new TMPro.TMP_InputField.OnValidateInput(ValidateInput);
            input.onSubmit.AddListener(new UnityEngine.Events.UnityAction<string>(HandleLineInput));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                bool previous = Parent.gameObject.activeSelf; //store the previous state of the controller
                //flip the state of the controller
                Parent.gameObject.SetActive(!Parent.gameObject.activeSelf);
                //if its active, set the inputfield as focused
                if (Parent.gameObject.activeSelf && !previous)
                {
                    //input.Select();
                    input.ActivateInputField();
                    input.stringPosition = caretPosition;
                }
                else if(!Parent.gameObject.activeSelf)
                {
                    //store the caret position so it remembers it when activated again
                    caretPosition = input.caretPosition;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                //close the window
                Parent.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            input.onSubmit.RemoveAllListeners();
            input.onValidateInput -= ValidateInput;
        }

        char ValidateInput(string s, int i, char c)
        {
            if (allowedChars.Contains(c)) { return c; }
            //else { Debug.Log("hmm? " + c); }
            return '\0';
        }

        void HandleLineInput(string line)
        {
            var so = ScriptableObject.CreateInstance<ConsoleCommandSO>();
            so.OutputEvent.AddListener(new UnityEngine.Events.UnityAction<string>(OutputMessage));
            so.debug = debugMode;
            so.scriptSource = line;
            so.Run();
            //while the script is running, remove the input line from the textbox
            input.SetTextWithoutNotify(string.Empty);

            //prevent the object avoiding garbage collection by removing event subs
            so.OutputEvent.RemoveAllListeners();

            input.Select();
        }

        void OutputMessage(string line)
        {
            string s = Output.text + "\n" + line;
            if (s.Length > outputCharLimit) s = s.Substring(s.Length - outputCharLimit);

            Output.text = s;
        }
    }
}
