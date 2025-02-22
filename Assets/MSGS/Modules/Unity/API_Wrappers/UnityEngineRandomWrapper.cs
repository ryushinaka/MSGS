using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable IDE0090
#pragma warning disable CS0165

namespace MiniScript.MSGS.Unity
{
    public class UnityEngineRandomWrapper
    {
        /// <summary>
        /// True for debug output to be generated for intrinsic calls
        /// </summary>
        public static bool debug = false;

        public static ValMap Get()
        {
            ValMap map = new ValMap();

            #region insideUnitCircle
            var a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.insideUnitCircle"); }
                Vector2 result = new Vector2();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.InsideUnitCircle;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (Vector2)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result.ToValMap());
            };
            map.map.Add(new ValString("insideUnitCircle"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "insideUnitCircle",
                "Returns a random point inside or on a circle with radius 1.0 (Read Only).",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValMap),
                    Comment = "The Vector2 (as a ValMap) of the point within the Circle."
                }
                );
            #endregion

            #region insideUnitSphere
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.insideUnitSphere"); }
                Vector3 result = new Vector3();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.InsideUnitSphere;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (Vector3)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result.ToValMap());
            };
            map.map.Add(new ValString("insideUnitSphere"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "insideUnitSphere",
                "Returns a random point inside or on a sphere with radius 1.0 (Read Only).",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValMap),
                    Comment = "The Vector3 (as a ValMap) of the point within the Sphere."
                }
            );
            #endregion

            #region onUnitSphere
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.onUnitSphere"); }
                Vector3 result = new Vector3();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.OnUnitSphere;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (Vector3)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result.ToValMap());
            };
            map.map.Add(new ValString("onUnitSphere"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "onUnitSphere",
                "Returns a random point on the surface of a sphere with radius 1.0 (Read Only).",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValMap),
                    Comment = "The Vector3 (as a ValMap) of the point on the Sphere."
                }
             );
            #endregion

            #region rotation
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.rotation"); }
                Quaternion result = new Quaternion();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.Rotation;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (Quaternion)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result.ToValMap());
            };
            map.map.Add(new ValString("rotation"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                    "rotation",
                    "Returns a random rotation (Read Only).",
                    "UnityEngine.Random",
                    new List<IntrinsicParameter>() { },
                    new IntrinsicParameter()
                    {
                        Name = "return",
                        variableType = typeof(ValMap),
                        Comment = "The Quaternion (as a ValMap) of the rotation."
                    }
            );
            #endregion

            #region rotationUniform
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.rotationUniform"); }
                Quaternion result = new Quaternion();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.RotationUniform;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (Quaternion)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result.ToValMap());
            };
            map.map.Add(new ValString("rotationUniform"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "rotationUniform",
                "Returns a random rotation with uniform distribution (Read Only).",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValMap),
                    Comment = "The Quaternion (as a ValMap) of the rotation."
                }
            );
            #endregion

            #region state
            a = Intrinsic.Create("");
            a.AddParam("vl", new ValList());
            a.code = (context, partialResult) =>
            {
                if (debug) { UnityEngine.Debug.Log("UnityEngine.Random.state"); }

                if (context.GetLocal("vl") != null)
                {
                    var vl = context.GetLocal("vl") as ValList;
                    UnityEngine.Random.State tmpState = UnityTypeExtensions.BaseRandomState();
                    if (vl != null && vl.values.Count == 16)
                    {
                        //convert the ValList to a Random.State
                        Random.State tmp = vl.ToRandomState();
                        var wi = AlternateThreadDispatcher.Get();
                        wi.Module = UnityModuleName.Random;
                        wi.FunctionName = UnityEngineRandomFunctions.State;
                        wi.args = new object[1] { (object)tmp }; //pass in the state object to assign

                        AlternateThreadDispatcher.Enqueue(ref wi);
                        wi.eventSlim.Wait();

                        tmpState = (UnityEngine.Random.State)wi.result;
                        ValList addendum = tmpState.ToValList();
                        AlternateThreadDispatcher.Return(ref wi);

                        return new Intrinsic.Result(addendum);
                    }
                    else
                    {
                        var wi = AlternateThreadDispatcher.Get();
                        wi.Module = UnityModuleName.Random;
                        wi.FunctionName = UnityEngineRandomFunctions.State;
                        wi.args = new object[0];
                        AlternateThreadDispatcher.Enqueue(ref wi);
                        wi.eventSlim.Wait();

                        UnityEngine.Random.State tmpState2 = (UnityEngine.Random.State)wi.result;
                        ValList addendum = tmpState2.ToValList();
                        AlternateThreadDispatcher.Return(ref wi);

                        return new Intrinsic.Result(addendum);
                    }
                }
                else
                {
                    //else no argument was given so we just return the current .state
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Random;
                    wi.FunctionName = UnityEngineRandomFunctions.State;

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();

                    UnityEngine.Random.State tmpState = (UnityEngine.Random.State)wi.result;
                    ValList addendum = tmpState.ToValList();
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(addendum);
                }

            };
            map.map.Add(new ValString("state"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "state",
                "Gets or sets the full internal state of the random number generator.",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValList),
                    Comment = "The ValList containing the byte values of the UnityEngine.Random.state serialized."
                }
            );
            #endregion

            #region value
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (UnityEngineRandomWrapper.debug) { Debug.Log("UnityEngine.Random.value"); }

                float result = float.NaN;

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.Value;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();

                result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("value"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "value",
                "Returns a random float within [0.0..1.0] (range is inclusive) (Read Only).",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() { },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(float),
                    Comment = "A float value between 0.0 and 1.0 (inclusively)."
                }
            );
            #endregion

            #region ColorHSV
            a = Intrinsic.Create("");
            a.AddParam("args", new ValList());
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.ColorHSV"); }

                Color randomColor = new Color();

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Random;
                wi.FunctionName = UnityEngineRandomFunctions.ColorHSV;
                ValList vargs = context.GetLocal("args") as ValList;

                switch (vargs.values.Count)
                {
                    case 0:
                        wi.args = new object[0]; break;
                    case 2:
                        wi.args = new object[2] { vargs.values[0].FloatValue(), vargs.values[1].FloatValue() };
                        break;
                    case 4:
                        wi.args = new object[4] {
                            vargs.values[0].FloatValue(),
                            vargs.values[1].FloatValue(),
                            vargs.values[2].FloatValue(),
                            vargs.values[3].FloatValue()
                        };
                        break;
                    case 6:
                        wi.args = new object[6] {
                            vargs.values[0].FloatValue(),
                            vargs.values[1].FloatValue(),
                            vargs.values[2].FloatValue(),
                            vargs.values[3].FloatValue(),
                            vargs.values[4].FloatValue(),
                            vargs.values[5].FloatValue()
                        };
                        break;
                    case 8:
                        wi.args = new object[8] {
                            vargs.values[0].FloatValue(),
                            vargs.values[1].FloatValue(),
                            vargs.values[2].FloatValue(),
                            vargs.values[3].FloatValue(),
                            vargs.values[4].FloatValue(),
                            vargs.values[5].FloatValue(),
                            vargs.values[6].FloatValue(),
                            vargs.values[7].FloatValue()
                        };
                        break;
                    default:
                        Debug.Log("UnityEngine.Random.ColorHSV was given too many arguments: " + vargs.values.Count);
                        break;
                }

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                randomColor = (Color)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(randomColor.ToValList());
            };

            map.map.Add(new ValString("ColorHSV"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "ColorHSV",
                "Generates a random color from HSV and alpha ranges.",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "hueMin",variableType = typeof(float),
                        Comment = "(Optional) A float value for the minimum hue value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "hueMax",variableType = typeof(float),
                        Comment = "(Optional) A float value for the maximum hue value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "saturationMin",variableType = typeof(float),
                        Comment = "(Optional) A float value for the minimum saturation value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "saturationMax",variableType = typeof(float),
                        Comment = "(Optional) A float value for the maximum saturation value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "valueMin",variableType = typeof(float),
                        Comment = "(Optional) A float value for the minimum value value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "valueMax",variableType = typeof(float),
                        Comment = "(Optional) A float value for the maximum value value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "alphaMin",variableType = typeof(float),
                        Comment = "(Optional) A float value for the minimum alpha value"
                    },
                    new IntrinsicParameter()
                    {
                        Name = "alphaMax",variableType = typeof(float),
                        Comment = "(Optional) A float value for the maximum alpha value"
                    },
                },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValList),
                    Comment = "A ValList containing the RGBA values of the color generated"
                }
            );
            #endregion

            #region InitState
            a = Intrinsic.Create("");
            a.AddParam("vl", (int)0);
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.InitState(" + context.GetLocalInt("vl") + ")"); }

                if (context.GetLocal("vl") != null)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Random;
                    wi.FunctionName = UnityEngineRandomFunctions.InitState;
                    wi.args = new object[1] { (int)context.GetLocalInt("vl") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();

                    AlternateThreadDispatcher.Return(ref wi);
                }

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("InitState"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "InitState",
                "Initializes the Random Number Generator with a seed value.",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() {
                        Name = "seed",
                        variableType = typeof(int),
                        Comment = "The seed value to initialze the Random Number Generator with."
                    }
                },
                null
            );
            #endregion

            #region RangeInt
            a = Intrinsic.Create("");
            a.AddParam("lower", int.MinValue);
            a.AddParam("upper", int.MaxValue);
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.RangeInt"); }

                if (context.GetLocal("lower") != null & context.GetLocal("upper") != null)
                {
                    int result = 0;

                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Random;
                    wi.FunctionName = UnityEngineRandomFunctions.RangeInt;
                    wi.args = new object[2] { (int)context.GetLocalFloat("lower"), (int)context.GetLocalFloat("upper") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();

                    result = (int)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(new ValNumber(result));
                }

                return new Intrinsic.Result(new ValNumber(float.NaN));
            };
            map.map.Add(new ValString("RangeInt"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "RangeInt",
                "Returns a random integer between 'lower' and 'upper' inclusively for both arguments.",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "lower",
                        variableType = typeof(int),
                        Comment = "The lesser number to set as the lowest inclusive bounds of the range."
                    },
                    new IntrinsicParameter()
                    {
                        Name = "upper",
                        variableType = typeof(int),
                        Comment = "The upper number to set as the highest inclusive bounds of the range"
                    }
                },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(int),
                    Comment = "The integer value generated by the RNG"
                }
            );
            #endregion

            #region RangeFloat
            a = Intrinsic.Create("");
            a.AddParam("lower", float.MinValue);
            a.AddParam("upper", float.MaxValue);
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Random.RangeFloat"); }
                if (context.GetLocal("lower") != null & context.GetLocal("upper") != null)
                {
                    float result = 0;

                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Random;
                    wi.FunctionName = UnityEngineRandomFunctions.RangeFloat;
                    wi.args = new object[2] { (float)context.GetLocalFloat("lower"), (float)context.GetLocalFloat("upper") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();

                    result = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(new ValNumber(result));
                }

                return new Intrinsic.Result(new ValNumber(float.NaN));
            };
            map.map.Add(new ValString("RangeFloat"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "RangeFloat",
                "Returns a random float between 'lower' and 'upper' inclusively for both arguments.",
                "UnityEngine.Random",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter()
                    {
                        Name = "lower",
                        variableType = typeof(float),
                        Comment = "The lesser number to set as the lowest inclusive bounds of the range."
                    },
                    new IntrinsicParameter()
                    {
                        Name = "upper",
                        variableType = typeof(float),
                        Comment = "The upper number to set as the highest inclusive bounds of the range"
                    }
                },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(float),
                    Comment = "The floating point value generated by the RNG"
                }
            );
            #endregion

            return map;
        }

        public static string GetDebugScriptSource()
        {
            return "vl = globals[\"Unity\"][\"Random\"].ColorHSV \r\n" +
            "globals[\"Unity\"][\"Random\"].ColorHSV [0.5,0.7] \r\n" +
            "globals[\"Unity\"][\"Random\"].ColorHSV [0.5,0.7,0.5,0.7] \r\n" +
            "globals[\"Unity\"][\"Random\"].ColorHSV [0.5,0.7,0.5,0.7,0.5,0.7] \r\n" +
            "globals[\"Unity\"][\"Random\"].ColorHSV [0.5,0.7,0.5,0.7,0.5,0.7,0.5,0.7] \r\n" +

            "f = globals[\"Unity\"][\"Random\"].insideUnitCircle \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].insideUnitSphere \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].onUnitSphere \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].rotation \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].rotationUniform \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].state \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            "f = globals[\"Unity\"][\"Random\"].value \r\n" +
            "globals[\"Host\"].LogInfo f \r\n" +

            //"globals[\"Unity\"][\"Random\"].InitState 123456 \r\n" +
            //"globals[\"Host\"].LogInfo \"InitStated\" \r\n" +

             "f = globals[\"Unity\"][\"Random\"].RangeInt(2, 6) \r\n" +
             "globals[\"Host\"].LogInfo f \r\n" +

             "f = globals[\"Unity\"][\"Random\"].RangeFloat(0, 1) \r\n" +
             "globals[\"Host\"].LogInfo f \r\n";
        }

        public static void HandleWorkItem(ref AlternateThreadWorkItem item)
        {
            if (item.Module != UnityModuleName.Random)
            {
                throw new System.ArgumentNullException("A work item was given to UnityEngine.Random that was not part of the Random callable namespace.");
            }

            switch ((int)item.FunctionName)
            {
                case UnityEngineRandomFunctions.ColorHSV:
                    #region
                    switch (item.args.Length)
                    {
                        case 0:
                            item.result = (object)UnityEngine.Random.ColorHSV();
                            break;
                        case 2:
                            item.result = (object)UnityEngine.Random.ColorHSV((float)item.args[0], (float)item.args[1]);
                            break;
                        case 4:
                            item.result = (object)UnityEngine.Random.ColorHSV((float)item.args[0], (float)item.args[1],
                                (float)item.args[2], (float)item.args[3]);
                            break;
                        case 6:
                            item.result = (object)UnityEngine.Random.ColorHSV((float)item.args[0], (float)item.args[1],
                                (float)item.args[2], (float)item.args[3], (float)item.args[4], (float)item.args[5]);
                            break;
                        case 8:
                            item.result = (object)UnityEngine.Random.ColorHSV((float)item.args[0], (float)item.args[1],
                                (float)item.args[2], (float)item.args[3], (float)item.args[4], (float)item.args[5],
                                (float)item.args[6], (float)item.args[7]);
                            break;
                        default:
                            break;
                    }
                    item.eventSlim.Set();
                    #endregion
                    break;
                case UnityEngineRandomFunctions.InitState:
                    UnityEngine.Random.InitState((int)item.args[0]);
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.InsideUnitCircle:
                    item.result = new Vector2();
                    item.result = UnityEngine.Random.insideUnitCircle;
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.InsideUnitSphere:
                    item.result = (object)UnityEngine.Random.insideUnitSphere;
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.OnUnitSphere:
                    item.result = (object)UnityEngine.Random.onUnitSphere;
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.RangeFloat:
                    item.result = (object)UnityEngine.Random.Range(
                        (float)item.args[0], (float)item.args[1]);
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.RangeInt:
                    item.result = (object)UnityEngine.Random.Range(
                        (int)item.args[0], (int)item.args[1]);
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.Rotation:
                    item.result = (object)UnityEngine.Random.rotation;
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.RotationUniform:
                    item.result = (object)UnityEngine.Random.rotationUniform;
                    item.eventSlim.Set();
                    break;
                case UnityEngineRandomFunctions.State:
                    if (item.args.Length == 1)
                    {
                        UnityEngine.Random.state = ((ValList)item.args[0]).ToRandomState();
                        item.eventSlim.Set();
                    }
                    else
                    {
                        item.result = (object)UnityEngine.Random.state;
                        item.eventSlim.Set();
                    }
                    break;
                case UnityEngineRandomFunctions.Value:
                    item.result = (object)UnityEngine.Random.value;
                    item.eventSlim.Set();
                    break;
                default:
                    throw new System.ArgumentNullException("A work item was given to UnityEngine.Random but the function was invalid.");
            }

            //if we throw with the default case, we still have to set the ManualResetEventSlim to unblock the waiting thread
            item.eventSlim.Set();
        }
    }
}
#pragma warning restore IDE0090
#pragma warning restore CS0165

