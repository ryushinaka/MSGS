using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.Editor
{
    public static class GuiUtils
    {
        public static bool SelectButtonList(ref Type selectedType, Type[] typesToDisplay)
        {
            var rect = GUILayoutUtility.GetRect(0, 25);

            for (int i = 0; i < typesToDisplay.Length; i++)
            {
                var name = typesToDisplay[i].Name;
                var btnRect = rect.Split(i, typesToDisplay.Length);

                if (GuiUtils.SelectButton(btnRect, name, typesToDisplay[i] == selectedType))
                {
                    selectedType = typesToDisplay[i];
                    return true;
                }
            }

            return false;
        }

        public static bool SelectEnumList(ref MiniScriptScriptableObjectType selectedType)
        {
            var rect = GUILayoutUtility.GetRect(0, 25);            
            var enums = Enum.GetValues(typeof(MiniScriptScriptableObjectType));
            
            for (int i = 0; i < enums.Length; i++)
            {
                var e = (MiniScriptScriptableObjectType)enums.GetValue(i);
                var name = e.ToString();
                var btnRect = rect.Split(i, enums.Length);

                if (GuiUtils.SelectButton(btnRect, name, enums.GetValue(i).ToString() == selectedType.ToString()))
                {
                    selectedType = (MiniScriptScriptableObjectType)enums.GetValue(i);
                    return true;
                }
            }

            return false;
        }

        public static bool SelectButton(Rect rect, string name, bool selected)
        {
            if(GUI.Button(rect, GUIContent.none, GUIStyle.none))
            {
                //Debug.Log("Chose " + name + " button.");
                return true;
            }

            if(Event.current.type == EventType.Repaint)
            {
                var style = new GUIStyle(EditorStyles.miniButtonMid);
                style.stretchHeight = true;
                style.fixedHeight = rect.height;
                style.Draw(rect, GUIHelper.TempContent(name), false, false, selected, false);
            }

            return false;
        }
    }
}

