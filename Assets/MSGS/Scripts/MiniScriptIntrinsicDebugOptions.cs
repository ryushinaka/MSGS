using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniScriptIntrinsicDebugOptions
{
    public System.Collections.BitArray bits = new BitArray(5, true);

    public bool enabled { get { return bits[0]; } set { bits[0] = value; } }
    public bool typecheck { get { return bits[1]; } set { bits[1] = value; } }
    public bool valuecheck { get { return bits[2]; } set { bits[2] = value; } }
    public bool rangecheck { get { return bits[3]; } set { bits[3] = value; } }
    public bool nullcheck { get { return bits[4]; } set { bits[4] = value; } }
}
