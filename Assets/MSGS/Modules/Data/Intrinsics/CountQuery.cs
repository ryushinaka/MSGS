using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.Database.Intrinsics
{
    public static class CountQuery
    {
        public static void Query(ref ValMap map)
        {
            var a = Intrinsic.Create("");
            #region            
            a.code = (context, partialResult) =>
            {
                //validate the 'propname' argument
                if (!string.IsNullOrEmpty(context.GetLocalString("propname")))
                {
                    context.interpreter.standardOutput("The property name ('" + context.GetLocalString("propname") + "') could not be found for the Count query.");
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
                    DatabaseModule.EnqueueQuery(chd.dbID, QueryCommandType.Count, null);
                }

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("Count"), a.GetFunc());
            #endregion
        }
    }
}