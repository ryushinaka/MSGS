using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Editor
{
    public class EditorConsoleCommand : ScriptableObject
    {
        [SerializeField]
        public string cmdName;
        public List<ConsoleCommandArgument> args;

        public bool TryParse(string line)
        {

            return false;
        }

        public static bool TryParse(string line, out EditorConsoleCommand ecc)
        {
            ecc = null;
            return false;
        }
    }

    public class ConsoleCommandArgument
    {

    }
}
