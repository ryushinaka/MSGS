#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class EditorBuildProcess : IPreprocessBuildWithReport
{
    // Specify the execution order of this build processor
    public int callbackOrder => 0;

    // Specify the component type you want to filter prefabs by
    private const string ComponentTypeName = "YourComponentName"; // Replace with your component name
    private const string OutputFilePath = "Assets/PrefabList.txt"; // Output file for the list

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("PrefabFinderBuildProcessor: Scanning for prefabs with specific component...");

        // Find all prefabs with the specified component
        var prefabPaths = FindPrefabsWithComponent(ComponentTypeName);

        // Save the list to a file
        SavePrefabListToFile(prefabPaths, OutputFilePath);

        Debug.Log($"PrefabFinderBuildProcessor: Found {prefabPaths.Count} prefabs with component {ComponentTypeName}. List saved to {OutputFilePath}.");
    }

    private List<string> FindPrefabsWithComponent(string componentTypeName)
    {
        List<string> prefabPaths = new List<string>();

        // Search for all prefab assets in the project
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in prefabGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            // Check if the prefab contains the specified component
            if (prefab != null && prefab.GetComponent(componentTypeName) != null)
            {
                prefabPaths.Add(path);
            }
        }

        return prefabPaths;
    }

    private void SavePrefabListToFile(List<string> prefabPaths, string filePath)
    {
        // Write prefab paths to the output file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string path in prefabPaths)
            {
                writer.WriteLine(path);
            }
        }

        // Ensure the asset database is updated to include the new file
        AssetDatabase.Refresh();
    }
}
#endif