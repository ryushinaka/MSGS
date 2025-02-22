using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace MiniScript.MSGS.ScriptableObjects
{
    public class MiniScriptValue : MiniScriptScriptableObject
    {
        [HideInInspector]
        public Value value;
        [HideInInspector]
        public MiniScriptVariableType Type;
    }

    public class MiniScriptValueDrawer : OdinValueDrawer<MiniScriptValue>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            //base.DrawPropertyLayout(label);
            if (ValueEntry.SmartValue.value == null) return;

            switch (ValueEntry.SmartValue.Type)
            {
                case MiniScriptVariableType.ValString:
                    ValueEntry.SmartValue.value = new ValString(EditorGUILayout.TextField(ValueEntry.SmartValue.value.ToString()));
                    break;

                case MiniScriptVariableType.ValInt32:
                    ValueEntry.SmartValue.value = new ValNumber(EditorGUILayout.IntField(ValueEntry.SmartValue.value.IntValue()));
                    break;

                case MiniScriptVariableType.ValFloat:
                    ValueEntry.SmartValue.value = new ValNumber(EditorGUILayout.FloatField(ValueEntry.SmartValue.value.FloatValue()));
                    break;

                case MiniScriptVariableType.ValDouble:
                    ValueEntry.SmartValue.value = new ValNumber(EditorGUILayout.DoubleField(ValueEntry.SmartValue.value.DoubleValue()));
                    break;

                case MiniScriptVariableType.ValBool:
                    ValueEntry.SmartValue.value = ValNumber.Truth(EditorGUILayout.Toggle(ValueEntry.SmartValue.value.BoolValue()));
                    break;
            }
        }
    }

}
