#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.Collections.Generic;

public class BundleFontsWindow : EditorWindow
{
    [SerializeField]
    private TMP_FontAsset[] selectedFonts;

    [MenuItem("Tools/BundleFonts")]
    public static void ShowWindow()
    {
        GetWindow<BundleFontsWindow>("Bundle TMP Fonts");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select TMP Font Assets to Bundle", EditorStyles.boldLabel);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty fontsProperty = so.FindProperty("selectedFonts");

        EditorGUILayout.PropertyField(fontsProperty, new GUIContent("Fonts"), true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Bundle Fonts as Addressables"))
        {
            BundleFonts();
        }
    }

    private void BundleFonts()
    {
        if (selectedFonts == null || selectedFonts.Length == 0)
        {
            EditorUtility.DisplayDialog("No Fonts Selected", "Please assign at least one TMP_FontAsset.", "OK");
            return;
        }

        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("AddressableAssetSettings not found. Make sure Addressables is set up.");
            return;
        }

        // Find or create a group for fonts
        AddressableAssetGroup fontGroup = settings.FindGroup("Fonts");
        if (fontGroup == null)
        {
            fontGroup = settings.CreateGroup("Fonts", false, false, false, null,
                typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
        }

        HashSet<TMP_FontAsset> allFontsToBundle = new HashSet<TMP_FontAsset>();

        foreach (var rootFont in selectedFonts)
        {
            if (rootFont == null) continue;

            var fonts = CollectFontAndFallbacksRecursive(rootFont);
            foreach (var font in fonts)
                allFontsToBundle.Add(font);
        }

        // Add all collected fonts (and their materials) to Addressables
        foreach (var font in allFontsToBundle)
        {
            string assetPath = AssetDatabase.GetAssetPath(font);
            var entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(assetPath), fontGroup);
            entry.address = font.name;

            if (font.material != null)
            {
                string matPath = AssetDatabase.GetAssetPath(font.material);
                settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(matPath), fontGroup);
            }
        }

        // Optional: Save and build Addressables
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("Fonts Bundled", "Font assets were added to the Fonts addressable group.", "OK");
    }

    private HashSet<TMP_FontAsset> CollectFontAndFallbacksRecursive(TMP_FontAsset root)
    {
        var collected = new HashSet<TMP_FontAsset>();
        var stack = new Stack<TMP_FontAsset>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var font = stack.Pop();
            if (font == null || collected.Contains(font))
                continue;

            collected.Add(font);

            if (font.fallbackFontAssetTable != null)
            {
                foreach (var fallback in font.fallbackFontAssetTable)
                {
                    if (fallback != null && !collected.Contains(fallback))
                        stack.Push(fallback);
                }
            }
        }

        return collected;
    }
}
#endif
