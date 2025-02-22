using UnityEngine;
using UnityEditor;

namespace MiniScript.MSGS.Editor
{
    public class MiniScriptScriptImporter : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            foreach (string str in importedAssets)
            {
                //Debug.Log("Imported Asset: " + str);
                

            }
            foreach (string str in deletedAssets)
            {
                //Debug.Log("Deleted Asset: " + str);
            }

            for (int i = 0; i < movedAssets.Length; i++)
            {
                //renaming counts as a Move with the movedAssets as "from" and the movedFromAssetPaths as "to"
                //Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
            }

            if (didDomainReload)
            {
                //Debug.Log("Domain has been reloaded");
            }
        }
    }

    
}
