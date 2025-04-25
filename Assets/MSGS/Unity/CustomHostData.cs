using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Database;

namespace MiniScript.MSGS.Unity
{
    /// <summary>
    /// Encapsulates objects/values so as to be accessable from the Interpreter.hostData property
    /// </summary>
    public class CustomHostData
    {
        //first thing we need to encapsulate is the Database reference
        //since every Interpreter gets a reference to the in-memory database
        //this is *REQUIRED* due to the lambda expression used to build Intrinsic calls
        //without a Database reference in the Interpreter's hostData property
        //there is no clean way to access the Database that I can find acceptable
        public string dbID;

        public CustomHostData()
        {
            dbID = System.Guid.NewGuid().ToString();
            //Database.DatabaseModule.workingContext.Add(dbID, new List<QueryCommand>());
        }
    }
}
