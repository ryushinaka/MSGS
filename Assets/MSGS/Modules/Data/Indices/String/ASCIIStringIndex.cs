using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MiniScript.MSGS.Data
{
    public class ASCIIStringIndex : BaseStringIndex
    {
        #region static char array declaration
        //all of the ascii characters
        public static readonly string[] asciicharsfull = new string[256] {
            "\0", "\x01", "\x02", "\x03", "\x04", "\x05", "\x06", "\x07",
            "\x08", "\t", "\n", "\x0B", "\x0C", "\r", "\x0E", "\x0F",
            "\x10", "\x11", "\x12", "\x13", "\x14", "\x15", "\x16", "\x17",
            "\x18", "\x19", "\x1A", "\x1B", "\x1C", "\x1D", "\x1E", "\x1F",
            " ", "!", "\"", "#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", "-", ".", "/",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?",
            "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_",
            "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "\x7F",
            "\x80", "\x81", "\x82", "\x83", "\x84", "\x85", "\x86", "\x87",
            "\x88", "\x89", "\x8A", "\x8B", "\x8C", "\x8D", "\x8E", "\x8F",
            "\x90", "\x91", "\x92", "\x93", "\x94", "\x95", "\x96", "\x97",
            "\x98", "\x99", "\x9A", "\x9B", "\x9C", "\x9D", "\x9E", "\x9F",
            "\xA0", "\xA1", "\xA2", "\xA3", "\xA4", "\xA5", "\xA6", "\xA7",
            "\xA8", "\xA9", "\xAA", "\xAB", "\xAC", "\xAD", "\xAE", "\xAF",
            "\xB0", "\xB1", "\xB2", "\xB3", "\xB4", "\xB5", "\xB6", "\xB7",
            "\xB8", "\xB9", "\xBA", "\xBB", "\xBC", "\xBD", "\xBE", "\xBF",
            "\xC0", "\xC1", "\xC2", "\xC3", "\xC4", "\xC5", "\xC6", "\xC7",
            "\xC8", "\xC9", "\xCA", "\xCB", "\xCC", "\xCD", "\xCE", "\xCF",
            "\xD0", "\xD1", "\xD2", "\xD3", "\xD4", "\xD5", "\xD6", "\xD7",
            "\xD8", "\xD9", "\xDA", "\xDB", "\xDC", "\xDD", "\xDE", "\xDF",
            "\xE0", "\xE1", "\xE2", "\xE3", "\xE4", "\xE5", "\xE6", "\xE7",
            "\xE8", "\xE9", "\xEA", "\xEB", "\xEC", "\xED", "\xEE", "\xEF",
            "\xF0", "\xF1", "\xF2", "\xF3", "\xF4", "\xF5", "\xF6", "\xF7",
            "\xF8", "\xF9", "\xFA", "\xFB", "\xFC", "\xFD", "\xFE", "\xFF"
        };

        //only the visible ascii characters
        public static readonly string[] asciicharsvisible = new string[97] {
            " ", "!", "\"", "#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", "-", ".", "/",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?",
            "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_",
            "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                                                                                //CR & LF are necessary here
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "\x13","\x10" };

        //public static readonly char[] asciicharsfull = new char[256] {
        //    '\0', '\x01', '\x02', '\x03', '\x04', '\x05', '\x06', '\x07',
        //    '\x08', '\t', '\n', '\x0B', '\x0C', '\r', '\x0E', '\x0F',
        //    '\x10', '\x11', '\x12', '\x13', '\x14', '\x15', '\x16', '\x17',
        //    '\x18', '\x19', '\x1A', '\x1B', '\x1C', '\x1D', '\x1E', '\x1F',
        //    ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
        //    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
        //    '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        //    'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
        //    '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
        //    'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '\x7F',
        //    '\x80', '\x81', '\x82', '\x83', '\x84', '\x85', '\x86', '\x87',
        //    '\x88', '\x89', '\x8A', '\x8B', '\x8C', '\x8D', '\x8E', '\x8F',
        //    '\x90', '\x91', '\x92', '\x93', '\x94', '\x95', '\x96', '\x97',
        //    '\x98', '\x99', '\x9A', '\x9B', '\x9C', '\x9D', '\x9E', '\x9F',
        //    '\xA0', '\xA1', '\xA2', '\xA3', '\xA4', '\xA5', '\xA6', '\xA7',
        //    '\xA8', '\xA9', '\xAA', '\xAB', '\xAC', '\xAD', '\xAE', '\xAF',
        //    '\xB0', '\xB1', '\xB2', '\xB3', '\xB4', '\xB5', '\xB6', '\xB7',
        //    '\xB8', '\xB9', '\xBA', '\xBB', '\xBC', '\xBD', '\xBE', '\xBF',
        //    '\xC0', '\xC1', '\xC2', '\xC3', '\xC4', '\xC5', '\xC6', '\xC7',
        //    '\xC8', '\xC9', '\xCA', '\xCB', '\xCC', '\xCD', '\xCE', '\xCF',
        //    '\xD0', '\xD1', '\xD2', '\xD3', '\xD4', '\xD5', '\xD6', '\xD7',
        //    '\xD8', '\xD9', '\xDA', '\xDB', '\xDC', '\xDD', '\xDE', '\xDF',
        //    '\xE0', '\xE1', '\xE2', '\xE3', '\xE4', '\xE5', '\xE6', '\xE7',
        //    '\xE8', '\xE9', '\xEA', '\xEB', '\xEC', '\xED', '\xEE', '\xEF',
        //    '\xF0', '\xF1', '\xF2', '\xF3', '\xF4', '\xF5', '\xF6', '\xF7',
        //    '\xF8', '\xF9', '\xFA', '\xFB', '\xFC', '\xFD', '\xFE', '\xFF'
        //};

        ////only the visible ascii characters
        //public static readonly char[] asciicharsvisible = new char[97] {
        //    ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
        //    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
        //    '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        //    'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
        //    '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
        //                                                                        //CR & LF are necessary here
        //    'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '\x13','\x10' };
        #endregion

        private List<string> strings = new List<string>();

        public Dictionary<char, CharacterElement> elements = new Dictionary<char, CharacterElement>();

        public override void OnCreate(ref ValString vs) { }

        public override void OnDelete(ref ValString vs) { }

        public override void OnUpdate(ref ValString vs) { }

        public (bool success, int index) Find(ref ValString vs, ref int index)
        {
            if (vs.value.Length > 0)
            {
                //abcd
                //GetCharacter(elements[index]);
            }

            return (false, -1);
        }

        public bool Find(ref string s, ref int index)
        {

            return false;
        }

        /// <summary>
        /// Creates a new StringIndex object that stores an index based on the 'length' parameter.
        /// </summary>
        /// <param name="length">How many characters to index into each string value stored</param>
        public ASCIIStringIndex()
        {
            string s = string.Empty;

            //while (length > 0)
            //{
            //    var c = s.GetEnumerator();
            //    //https://github.com/BeForU/UDE-for-Unity
            //    //to index it correctly, every char must be UTF32
            //    //but UTF32 is a *string* encoding, and a char may or may not be a surrogate or a surrogate pair
            //    //.NET seems to handle this internally somehow...
            //    length--;
            //}
        }

        public void Init(uint depth, bool full)
        {  
            if (full) { Init(depth, asciicharsfull); }
            else { Init(depth, asciicharsvisible); }
        }

        public void Init(uint depth, string[] characters)
        {
            string rst = string.Empty;
            //uint counter = 0;

            foreach (string s1 in characters)
            {   
                elements.Add(s1[0], new CharacterElement());

                foreach(string s2 in characters)
                {
                    var ce1 = elements[s1[0]];
                    ce1.children = new Dictionary<char, CharacterElement>();
                    ce1.key[0] = s1[0];
                    
                    foreach(string s3 in characters)
                    {
                        var ce2 = ce1.children[s2[0]];
                        ce2.children = new Dictionary<char, CharacterElement>();
                        ce2.key[0] = s3[0];
                    }
                }
            }
        }

        public static string PrettyPrintDictionary(ref Dictionary<char, CharacterElement> dict, int indentLevel)
        {
            var indent = new string(' ', indentLevel * 2);
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("{");
            foreach (var kvp in dict)
            {
                sb.Append($"{indent}  \"{kvp.Key}\": ");
                if (kvp.Value is CharacterElement nestedDict)
                {
                    sb.AppendLine();
                    sb.Append(PrettyPrintDictionary(ref nestedDict.children, indentLevel + 1));
                }
                else
                {
                    sb.AppendLine($"\"{kvp.Value}\",");
                }
            }
            sb.AppendLine($"{indent}}}");
            return sb.ToString();
        }

        void PopulateCharacters(ref string[] characters, ref uint depth, ref uint depthcount, ref CharacterElement ce, ref string charkey)
        {
            //since this is ASCII only, we will assume that each element is a single char
            //no surrogates or pairs, etc.
            if (depthcount < depth)
            {
               // if (charkey[0] != '\x10' || charkey[0] != '\x13') {
                    ce.key[0] = charkey[0]; //set the index character

                    foreach (string s in characters)
                    {
                        string stroup = s;
                        CharacterElement child = new CharacterElement();
                        ce.parent = ce;
                        ce.children.Add(s[0], child);
                        PopulateCharacters(ref characters, ref depth, ref depthcount, ref child, ref stroup);
                    }

                    depthcount++;
                //}
            }
        }

        bool ValidCharacters(ref string[] characters, out string msg)
        {
            bool result = false;
            msg = string.Empty;
            int c = 0;
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].Length == 1)
                {
                    c = System.Convert.ToInt32(characters[i]);

                }
                else
                {
                    result = false;
                    msg = "Only single/individual ASCII characters allowed per element in the array. The array contains" +
                        "multiple characters at index [" + i + "].";
                    break;
                }

            }
            return result;
        }

        class Fragment
        {
            public static readonly string[] asciicharsfull = new string[256] {
            "\0", "\x01", "\x02", "\x03", "\x04", "\x05", "\x06", "\x07",
            "\x08", "\t", "\n", "\x0B", "\x0C", "\r", "\x0E", "\x0F",
            "\x10", "\x11", "\x12", "\x13", "\x14", "\x15", "\x16", "\x17",
            "\x18", "\x19", "\x1A", "\x1B", "\x1C", "\x1D", "\x1E", "\x1F",
            " ", "!", "\"", "#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", "-", ".", "/",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?",
            "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_",
            "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "\x7F",
            "\x80", "\x81", "\x82", "\x83", "\x84", "\x85", "\x86", "\x87",
            "\x88", "\x89", "\x8A", "\x8B", "\x8C", "\x8D", "\x8E", "\x8F",
            "\x90", "\x91", "\x92", "\x93", "\x94", "\x95", "\x96", "\x97",
            "\x98", "\x99", "\x9A", "\x9B", "\x9C", "\x9D", "\x9E", "\x9F",
            "\xA0", "\xA1", "\xA2", "\xA3", "\xA4", "\xA5", "\xA6", "\xA7",
            "\xA8", "\xA9", "\xAA", "\xAB", "\xAC", "\xAD", "\xAE", "\xAF",
            "\xB0", "\xB1", "\xB2", "\xB3", "\xB4", "\xB5", "\xB6", "\xB7",
            "\xB8", "\xB9", "\xBA", "\xBB", "\xBC", "\xBD", "\xBE", "\xBF",
            "\xC0", "\xC1", "\xC2", "\xC3", "\xC4", "\xC5", "\xC6", "\xC7",
            "\xC8", "\xC9", "\xCA", "\xCB", "\xCC", "\xCD", "\xCE", "\xCF",
            "\xD0", "\xD1", "\xD2", "\xD3", "\xD4", "\xD5", "\xD6", "\xD7",
            "\xD8", "\xD9", "\xDA", "\xDB", "\xDC", "\xDD", "\xDE", "\xDF",
            "\xE0", "\xE1", "\xE2", "\xE3", "\xE4", "\xE5", "\xE6", "\xE7",
            "\xE8", "\xE9", "\xEA", "\xEB", "\xEC", "\xED", "\xEE", "\xEF",
            "\xF0", "\xF1", "\xF2", "\xF3", "\xF4", "\xF5", "\xF6", "\xF7",
            "\xF8", "\xF9", "\xFA", "\xFB", "\xFC", "\xFD", "\xFE", "\xFF"
        };

            //only the visible ascii characters
            public static readonly string[] asciicharsvisible = new string[97] {
            " ", "!", "\"", "#", "$", "%", "&", "\"", "(", ")", "*", "+", ",", "-", ".", "/",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?",
            "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_",
            "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                                                                                //CR & LF are necessary here
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "\x13","\x10" };

            public static CharacterElement GetElement(char key, bool full)
            {
                CharacterElement ce = new CharacterElement();
                ce.key[0] = key;
                if(full)
                {
                    foreach (string s in asciicharsfull)
                    {
                        CharacterElement child = new CharacterElement();
                        child.key[0] = s[0];
                        ce.children.Add(s[0], child);
                    }
                }
                else
                {   //visible only
                    foreach(string s in asciicharsvisible)
                    {
                        CharacterElement child = new CharacterElement();
                        child.key[0] = s[0];
                        ce.children.Add(s[0], child);
                    }
                }
                
                return ce;
            }
        }
    }





    public class CharacterGraph
    {
        List<string> strings;
        uint depth = 0;
        Dictionary<char, CharacterElement> elements = new Dictionary<char, CharacterElement>();
        bool isDepthSet = false;

        public void TryAdd(ref string s, out bool success)
        {
            if (s.Length == 0) { success = false; return; }

            if (!Contains(ref s))
            {
                if (elements.ContainsKey(s[0]))
                {

                }
            }

            success = false;
        }

        public bool Contains(ref string s)
        {
            return strings.Contains(s);
        }

        public void Remove(ref string s)
        {

        }

        /// <summary>
        /// How many characters of each string to index
        /// </summary>
        /// <param name="d">How many characters of each string to index</param>
        /// <remarks>Changing this value after the index has already been generated will cause the index to be regenerated!
        /// you have been warned.</remarks>
        public string setDepth(uint d, List<char> keys)
        {
            Dictionary<char, object> tmp = new Dictionary<char, object>();
            PopulateDictionaryRecursively(keys, 0, tmp);

            int idx = 0;
            foreach (char c in keys)
            {

                idx++;
            }

            return PrettyPrintDictionary(tmp, 0);
        }

        static string PrettyPrintDictionary(Dictionary<char, object> dict, int indentLevel)
        {
            var indent = new string(' ', indentLevel * 2);
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("{");
            foreach (var kvp in dict)
            {
                sb.Append($"{indent}  \"{kvp.Key}\": ");
                if (kvp.Value is Dictionary<char, object> nestedDict)
                {
                    sb.AppendLine();
                    sb.Append(PrettyPrintDictionary(nestedDict, indentLevel + 1));
                }
                else
                {
                    sb.AppendLine($"\"{kvp.Value}\",");
                }
            }
            sb.AppendLine($"{indent}}}");
            return sb.ToString();
        }

        void PopulateDictionaryRecursively(List<char> charList, int index, Dictionary<char, object> charDict)
        {
            if (index < charList.Count)
            {
                // Add the current character to the dictionary
                charDict[charList[index]] = new object();

                // Recurse to the next character
                PopulateDictionaryRecursively(charList, index + 1, charDict);
            }
        }
    }

    public class CharacterElement
    {
        public CharacterElement parent;
        public Dictionary<char, CharacterElement> children = new Dictionary<char, CharacterElement>();

        public char[] key = new char[2];
        public List<WeakReference<string>> elements = new List<WeakReference<string>>();

        public CharacterElement Parent
        {
            get { return parent; }
            private set { }
        }

        public CharacterElement this[char c]
        {
            get { return null; }
            private set { }
        }
        public CharacterElement this[string s]
        {
            get { return null; }
            private set { }
        }

        public CharacterElement Children(char c)
        {
            return null;
        }
        public CharacterElement Children(string s)
        {
            return null;
        }
    }
}


