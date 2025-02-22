using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.Database.Intrinsics
{
    public static class AsListQuery
    {
        public static void Query (ref ValMap map)
        {
            var a = Intrinsic.Create("");
            #region
            a.AddParam("dbid", new ValString(string.Empty));
            a.AddParam("property", new ValString(string.Empty));
            a.code = (context, partialResult) =>
            {
                var chd = (CustomHostData)context.interpreter.hostData;
                if (chd == null)
                {
                    context.interpreter.errorOutput(
                        "CustomHostData object not found in the Interpreter, unable to access the database."
                        );
                }
                else
                {
                    DatabaseModule.EnqueueQuery(context.GetLocalString("dbid"), QueryCommandType.AsList,
                    context.GetLocalString("property"));
                    //we will automatically append the .End() to the query here, for script author convenience
                    var result =  DatabaseModule.EnqueueQuery(context.GetLocalString("dbid"), QueryCommandType.End, null);
                    if(result is ValList) { return new Intrinsic.Result((ValList)result); }
                    else if(result is ValMap) { return new Intrinsic.Result((ValMap)result); }
                    return new Intrinsic.Result(null);

                }

                return new Intrinsic.Result(null);
            };
            #endregion
        }
    }
}
