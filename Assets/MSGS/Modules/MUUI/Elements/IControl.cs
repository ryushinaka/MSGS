using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;

namespace MiniScript.MSGS.MUUI
{
    public interface IControl
    {
        ValMap GetValMap { get; set; }
        ValMap GetProperties();
    }
}

