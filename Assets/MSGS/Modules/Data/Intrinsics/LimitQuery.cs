using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.Database.Intrinsics
{
    public class LimitQuery : MonoBehaviour
    {
        public static void Query(ref ValMap map)
        {
            var a = Intrinsic.Create("");
            #region
            a.AddParam("quantity", new ValString("0"));
            a.code = (context, partialResult) =>
            {
                //validate the 'propname' argument
                if (!string.IsNullOrEmpty(context.GetLocalString("quantity")))
                {
                    context.interpreter.standardOutput("The property value for 'quantity' could not be found for the Limit query.");
                    //return early, nothing we can do with a logically invalid argument
                    return new Intrinsic.Result(null);
                }

                //pull the .hostData object and check
                var chd = (CustomHostData)context.interpreter.hostData;
                if (chd == null)
                {
                    context.interpreter.errorOutput(
                        "CustomHostData object not found in the Interpreter, unable to access the database."
                        );
                }
                else
                {
                    int limit = 0;
                    if (!int.TryParse(context.GetLocalString("quantity"), out limit))
                    {
                        //failed to parse an integer value from the string given, inform the host environment
                        context.interpreter.errorOutput(
                            "Unable to parse an integer value from the string value (" + context.GetLocalString("quantity") + ") for the Limit query."
                            );
                    }
                    else { DatabaseModule.EnqueueQuery(chd.dbID, QueryCommandType.Limit, limit); }
                }

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("Limit"), a.GetFunc());
            #endregion
        }
    }
}