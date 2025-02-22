using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.PackageManager
{
    public class ScriptFileChecker
    {
        bool needsInput = false;
        bool flagerror = false;
        public MiniScriptException exception;

        public bool Incomplete { get { return needsInput; } }
        public bool HasErrOutput { get { return flagerror; } }

        public void ValidateScript(string script)
        {
            //reset flags first if we're being called multiple times
            flagerror = false; needsInput = false; exception = null;

            var parser = new Parser();
            try {
                //we just need the parser for this validation
                parser.Parse(script, false);
                //its possible there is an incomplete statement in the script, so we check for that
                if(parser.NeedMoreInput()) { needsInput = true; }
            }
            catch (MiniScriptException mse) 
            {
                flagerror = true;
                exception = mse;
            }
        }
    }
}
