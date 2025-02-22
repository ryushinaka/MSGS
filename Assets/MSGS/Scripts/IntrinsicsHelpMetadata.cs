using System;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;

namespace MiniScript.MSGS
{
    public static class IntrinsicsHelpMetadata
    {
        static List<IntrinsicMetadata> _Intrinsics;

        public static bool Contains(string name)
        {
            for (int i = 0; i < _Intrinsics.Count; i++)
            {
                if (_Intrinsics[i].Name == name) { return true; }
            }
            return false;
        }

        public static void Create(string name, string description, string module, List<IntrinsicParameter> args, IntrinsicParameter returns)
        {
            if (!Contains(name))
            {
                _Intrinsics.Add(new IntrinsicMetadata()
                {
                    Name = name,
                    Description = description,
                    Module = module,
                    Parameters = args,
                    Returns = returns
                });
            }
        }

        public static void Clear() { _Intrinsics.Clear(); }

        public static IntrinsicMetadata Get(string name)
        {
            if (Contains(name))
            {
                for (int i = 0; i < _Intrinsics.Count; i++)
                {
                    if (_Intrinsics[i].Name == name)
                    {
                        return _Intrinsics[i];
                    }
                }
            }

            return null;
        }

        public static List<string> GetCatalog()
        {
            List<string> rst = new List<string>();
            foreach(IntrinsicMetadata imd in _Intrinsics)
            {
                rst.Add(imd.Module + "/" + imd.Name);
            }

            return rst;
        }

        static IntrinsicsHelpMetadata()
        {
            _Intrinsics = new List<IntrinsicMetadata>();
        }
    }

    public class IntrinsicMetadata
    {
        public string Name;
        public string Description;
        public string Module;
        public List<IntrinsicParameter> Parameters;
        public IntrinsicParameter Returns;
    }

    public class IntrinsicParameter
    {
        public string Name;
        public string Comment;
        public Type variableType;
    }
}

