using System;

namespace MiniScript.MSGS
{
    public class ManageableAttribute : System.Attribute { }

    public interface IsListAttribute
    {
        string GetAddress { get; set; }

        ValList GetValList();
    }

    public interface IsDictionaryAttribute
    {
        string GetAddress { get; set; }

        ValMap GetValMap();
    }
}




