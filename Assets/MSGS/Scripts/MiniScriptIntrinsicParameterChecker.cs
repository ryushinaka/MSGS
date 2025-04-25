using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniScript.MSGS
{
    /// <summary>
    /// Validates that the argument(s) given to an Intrinsic call meet expectations
    /// </summary>
    public static class MiniScriptIntrinsicParameterChecker
    {   //float and double epsilon values
        public static float fepsilon = 0.00001f, depsilon = 0.000001f;

        public static bool TryNullCheck(this TAC.Context context, string name) =>
            context?.GetLocal(name) != null;
        public static bool TryTypeCheck(this TAC.Context context, string name, Type type)
        {
            var o = context?.GetLocal(name);
            return o != null && type.IsInstanceOfType(o);
        }
        public static bool TryValueCheck<T>(this TAC.Context context, string name, T value)
        {
            return value switch
            {
                string s => context.GetLocalString(name) == s,
                int i => context.GetLocalInt(name) == i,
                double d => context.GetLocal(name).DoubleValue() == d,
                float f => context.GetLocal(name).FloatValue() == f,
                bool b => context.GetLocal(name).BoolValue() == b,
                _ => throw new NotSupportedException($"Type {typeof(T)} is not supported in IntrinsicParameterChecker.TryValueCheck")
            };
        }
        public static bool TryRangeCheck<T>(this TAC.Context context, string name, int low, int high, bool inclusive)
        {
            if (typeof(T) == typeof(string))
            {
                string strValue = context.GetLocalString(name);
                if (strValue == null) return false;
                return inclusive ? (strValue.Length >= low && strValue.Length <= high) : (strValue.Length > low && strValue.Length < high);
            }
            else if (typeof(T) == typeof(int))
            {
                int v = context.GetLocalInt(name);
                return inclusive ? (v >= low && v <= high) : (v > low && v < high);
            }
            else
            {
                MiniScriptSingleton.LogWarning("IntrinsicParameterCheck.TryValueCheck does not support " + typeof(T).FullName + " type.");
            }

            return false;
        }
        public static bool TryRangeCheck(this TAC.Context context, string name, float low, float high, bool inclusive) {
            float i = (float)context?.GetLocalFloat(name);
            if (inclusive)
            {
                return (i > low || System.Math.Abs(i - low) < fepsilon) &&
                       (i < high || System.Math.Abs(i - high) < fepsilon);
            }
            else { return (i > low + fepsilon) && (i < high - fepsilon); }
        }
        public static bool TryRangeCheck(this TAC.Context context, string name, double low, double high, bool inclusive) {
            double vv = (double)(context?.GetLocal(name).DoubleValue());
            if (inclusive)
            {
                return (vv > low || System.Math.Abs(vv - low) < depsilon) &&
                       (vv < high || System.Math.Abs(vv - high) < depsilon);
            }
            else { return (vv > low + depsilon) && (vv < high - depsilon); }
        }
        public static bool TryValueCheck(this TAC.Context context, string name, string value) =>
            context?.GetLocalString(name)?.Equals(value, StringComparison.OrdinalIgnoreCase) ?? false;
        public static bool TryValueCheck(this TAC.Context context, string name, System.Text.RegularExpressions.Regex regex)
        {
            var s = context?.GetLocalString(name);
            return s != null && regex?.IsMatch(s) == true;
        }

        /// <summary>
        /// Attempts to validate the 'context' parameters against the 'metadata' expectations
        /// </summary>
        /// <param name="context">The TAC.Context of the Interpreter</param>
        /// <param name="metadata">The 'metadata' of the Intrinsic method being validated against</param>
        /// <param name="msg">If return value is 'false', then the string 'msg' will explain why it was false.</param>
        /// <returns></returns>
        public static bool TryValidate(ref TAC.Context context, ref IntrinsicMetadata metadata, ref string msg)
        {

            if (MiniScriptSingleton.intrinsicDebugOptions.enabled)
            {
                bool passes = false;
                for (int z = 0; z < metadata.Parameters.Count; z++)
                {
                    if (MiniScriptSingleton.intrinsicDebugOptions.nullcheck) passes |= context.TryNullCheck(metadata.Parameters[z].Name);
                    if (MiniScriptSingleton.intrinsicDebugOptions.typecheck) passes |= context.TryTypeCheck(metadata.Parameters[z].Name, metadata.Parameters[z].variableType);
                    if (MiniScriptSingleton.intrinsicDebugOptions.valuecheck && MiniScriptSingleton.intrinsicDebugOptions.rangecheck)
                    {

                        if (metadata.Parameters[z].variableType == typeof(string))
                        {
                            passes |= context.TryRangeCheck<string>(metadata.Parameters[z].Name,
                                (int)metadata.Parameters[z].lrange,
                                (int)metadata.Parameters[z].urange,
                                metadata.Parameters[z].rangeinclusive);
                        }
                        else if (metadata.Parameters[z].variableType == typeof(float))
                        {
                            passes |= context.TryRangeCheck(metadata.Parameters[z].Name,
                                (float)metadata.Parameters[z].lrange,
                                (float)metadata.Parameters[z].urange,
                                metadata.Parameters[z].rangeinclusive);
                        }
                        else if (metadata.Parameters[z].variableType == typeof(double))
                        {
                            passes |= context.TryRangeCheck(metadata.Parameters[z].Name,
                                (int)metadata.Parameters[z].lrange,
                                (int)metadata.Parameters[z].urange,
                                metadata.Parameters[z].rangeinclusive);
                        }
                        else if (metadata.Parameters[z].variableType == typeof(int))
                        {
                            passes |= context.TryRangeCheck<int>(metadata.Parameters[z].Name,
                                (int)metadata.Parameters[z].lrange,
                                (int)metadata.Parameters[z].urange,
                                metadata.Parameters[z].rangeinclusive);
                        }
                        else if (metadata.Parameters[z].variableType == typeof(ValList))
                        {
                            passes |= true;
                        }
                        else if (metadata.Parameters[z].variableType == typeof(ValMap))
                        {
                            passes |= true;
                        }
                        else { MiniScriptSingleton.LogWarning("#1 IntrinsicValidator.TryValidate was given an unhandled type: " + metadata.Parameters[z].variableType.FullName); }
                    }
                    else if (MiniScriptSingleton.intrinsicDebugOptions.valuecheck && !MiniScriptSingleton.intrinsicDebugOptions.rangecheck)
                    {
                        passes |= context.GetLocalString(metadata.Parameters[z].Name).Length > 0;
                    }
                }

                return passes;
            }

            //return true by default (though I suspect this is madness)
            return true;
        }
    }

}
