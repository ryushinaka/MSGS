using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.Database.Intrinsics
{
    public static class SelectQuery
    {
        public static void Query(ref ValMap map)
        {
            var a = Intrinsic.Create("");
            #region
            a.AddParam("typename", new ValString(string.Empty));
            a.code = (context, partialResult) =>
            {
                //validate the 'typename' argument, ensure that a declared Type actually exists that matches the name
                if (!Data.DataStoreWarehouse.Contains(context.GetLocalString("typename")))
                {
                    context.interpreter.standardOutput("The Type name ('" + context.GetLocalString("typename") + "') could not be found for the Select query.");
                    //return early, nothing we can do with a logically invalid argument
                    return new Intrinsic.Result(null);
                }

                //its a valid argument, so lets pull the .hostData object and check
                var chd = (CustomHostData)context.interpreter.hostData;
                if (chd == null)
                {
                    context.interpreter.errorOutput(
                        "CustomHostData object not found in the Interpreter, unable to access the database."
                        );
                }
                else
                {
                    DatabaseModule.EnqueueQuery(chd.dbID, QueryCommandType.Select,
                    context.GetLocalString("typename"));
                }

                return new Intrinsic.Result(null);
            };

            map.map.Add(new ValString("Select"), a.GetFunc());
            #endregion
        }
    }
}

