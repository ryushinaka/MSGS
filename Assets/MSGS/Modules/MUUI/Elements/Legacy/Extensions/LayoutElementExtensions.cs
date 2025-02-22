using UnityEngine.UI;
using System.Data;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI.Extensions
{
    public static class LayoutElementExtensions
    {
        public static ValMap ToValMap(this LayoutElement layout)
        {
            ValMap vm = new ValMap();
            vm.map.Add(new ValString("InstanceID"), new ValNumber(layout.gameObject.GetInstanceID()));
            vm.map.Add(new ValString("enabled"), ValNumber.Truth(layout.enabled));
            vm.map.Add(new ValString("IgnoreLayout"), ValNumber.Truth(layout.ignoreLayout));
            vm.map.Add(new ValString("MinWidth"), new ValNumber(layout.minWidth));
            vm.map.Add(new ValString("MinHeight"), new ValNumber(layout.minHeight));
            vm.map.Add(new ValString("PreferredWidth"), new ValNumber(layout.preferredWidth));
            vm.map.Add(new ValString("PreferredHeight"), new ValNumber(layout.preferredHeight));
            vm.map.Add(new ValString("FlexibleWidth"), new ValNumber(layout.flexibleWidth));
            vm.map.Add(new ValString("FlexibleHeight"), new ValNumber(layout.flexibleHeight));
            vm.map.Add(new ValString("LayoutPriority"), new ValNumber(layout.layoutPriority));

            return vm;
        }

        public static bool UpdateLayout(this LayoutElement layout, Value a, Value b)
        {
            if(a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "enabled":
                        #region
                        if (b is ValNumber)
                        {
                            layout.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "IgnoreLayout":
                        #region
                        if (b is ValNumber)
                        {
                            layout.ignoreLayout = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").IgnoreLayout property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "MinWidth":
                        #region
                        if (b is ValNumber)
                        {   
                            layout.minWidth = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").MinWidth property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                        #endregion
                    case "MinHeight":
                        #region
                        if (b is ValNumber)
                        {
                            layout.minHeight = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").MinHeight property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                        #endregion
                    case "PreferredWidth":
                        #region
                        if (b is ValNumber)
                        {
                            layout.preferredWidth = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").PreferredWidth property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                        #endregion
                    case "PreferredHeight":
                        #region
                        if (b is ValNumber)
                        {
                            layout.preferredHeight = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").PreferredHeight property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                        #endregion
                    case "FlexibleWidth":
                        #region
                        if (b is ValNumber)
                        {
                            layout.flexibleWidth = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").FlexibleWidth property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FlexibleHeight":
                        #region
                        if (b is ValNumber)
                        {
                            layout.flexibleHeight = ((ValNumber)b).FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").FlexibleHeight property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "LayoutPriority":
                        #region
                        if (b is ValNumber)
                        {
                            layout.layoutPriority = ((ValNumber)b).IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ").LayoutPriority property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                        #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Button.LayoutElement(" + layout.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupLayout(this LayoutElement layout, ref DataRow row)
        {
            layout.enabled = bool.Parse(row["enabled"].ToString());
            layout.ignoreLayout = bool.Parse(row["IgnoreLayout"].ToString());
            layout.minWidth = float.Parse(row["IgnoreLayout"].ToString());
            layout.minHeight = float.Parse(row["IgnoreLayout"].ToString());
            layout.preferredWidth = float.Parse(row["IgnoreLayout"].ToString());
            layout.preferredHeight = float.Parse(row["IgnoreLayout"].ToString());
            layout.flexibleWidth = float.Parse(row["IgnoreLayout"].ToString());
            layout.flexibleHeight = float.Parse(row["IgnoreLayout"].ToString());
            layout.layoutPriority = int.Parse(row["IgnoreLayout"].ToString());
        }
    }
}
