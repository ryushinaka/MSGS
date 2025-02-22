using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using MiniScript.MSGS.Zip;
using MiniScript.MSGS;

namespace MiniScript.MSGS.PackageManager
{
    /// <summary>
    /// Used to create new Manifest documents for 'Export Package' functionality of MSGS
    /// </summary>
    /// <remarks>A valid manifest.xml is required for the package zip file to be recognized as valid.</remarks>
    public class ManifestFactory
    {
        /// <summary>
        /// Parses the contents of the path for the contents of the Package
        /// </summary>
        /// <param name="path"></param>
        /// <remarks>This does compile and validate the scripts, as well as the spritesheets and sound collections.</remarks>
        public Manifest Path(string path, bool debug)
        {
            Manifest entries = new Manifest();

            List<string> result = new List<string>(System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories));
            //use the checker objects to validate the files for inclusion with the packaged zip file
            IManifestFileChecker fchecker = new DefaultManifestFileChecker();
            IDataFileChecker dchecker = new DefaultDataFileChecker();
            ScriptFileChecker schecker = new ScriptFileChecker();

            for (int i = 0; i < result.Count; i++)
            {
                //UnityEngine.Debug.Log(result[i]);
                if (fchecker.ApprovedFile(result[i]))
                {
                    //check which kind of file it is
                    var mf = new ManifestEntry()
                    {
                        filename = System.IO.Path.GetFileName(result[i]),
                        path = result[i],
                        ftype = fchecker.GetFileType(result[i]),
                        dtypes = dchecker.GetDataFileType(result[i])
                    };

                    //does the script pass the parser/compiler check?
                    if (mf.ftype == FileTypes.Script)
                    {
                        schecker.ValidateScript(System.IO.File.ReadAllText(result[i]));
                        if (schecker.exception != null || schecker.HasErrOutput || schecker.Incomplete)
                        {
                            mf.validated = false;
                        }
                        else 
                        {
                            mf.validated = true;
                            entries.AddEntry(mf);
                        }
                    }
                    else if(mf.ftype == FileTypes.Data)
                    {
                        entries.AddEntry(mf);
                    }
                    else if (mf.ftype == FileTypes.Image)
                    {
                        entries.AddEntry(mf);
                    }
                    else if (mf.ftype == FileTypes.Model3D)
                    {
                        entries.AddEntry(mf);
                    }
                    else if (mf.ftype == FileTypes.Sound)
                    {
                        entries.AddEntry(mf);
                    }
                    else if (mf.ftype == FileTypes.Unknown)
                    {

                    }
                }
            }

            return entries;
        }
    }

    public class ManifestEntry
    {
        public string path;
        public string filename;
        public bool validated;
        public FileTypes ftype;
        public DataFileTypes dtypes;

        public override string ToString()
        {
            return "path: " + path + " || file: " + filename + " || valid: " + validated + " || " + ftype.ToString() + " || " + dtypes.ToString();
        }
    }
}
