using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MiniScript.MSGS.PackageManager
{
    public interface IDataFileChecker
    {
        DataFileTypes GetDataFileType(string filepath);

        DataFileTypes GetDataFileType(ref StreamReader stream);
    }
}

