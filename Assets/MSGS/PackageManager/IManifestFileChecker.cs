using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.PackageManager
{
    public interface IManifestFileChecker
    {
        bool ApprovedFile(string filepath);

        FileTypes GetFileType(string filepath);

        DataFileTypes GetDataFileType(string filepath);
    }
}

