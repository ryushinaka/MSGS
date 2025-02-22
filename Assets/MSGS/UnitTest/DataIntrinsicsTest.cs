using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Data;

namespace MiniScript.MSGS.Testing
{
    public static class DataIntrinsicsTest
    {        
        public static void DoTest()
        {
            //build the script source
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            #region script source
            str.AppendLine("globals[\"data\"].CreateDataStore(\"test\")");            
            str.AppendLine("globals[\"data\"].GetTypeStoreList");
            //str.AppendLine("globals[\"data\"].Select(\"test\")");
            //str.AppendLine("globals[\"data\"].SelectRegx(\"test\")");
            //str.AppendLine("globals[\"data\"].SelectRange(\"test\")");
            str.AppendLine("globals[\"data\"].GetRandomInstance(\"test\")");
            str.AppendLine("globals[\"data\"].GetRandomInstances(\"test\", 10)");            
            str.AppendLine("globals[\"data\"].RemoveInstance(\"test\", null)");
            str.AppendLine("globals[\"data\"].RemoveInstances(\"test\", null)");
            str.AppendLine("globals[\"data\"].CreateInstance(\"test\")");
            str.AppendLine("globals[\"data\"].CreateInstances(\"test\", 10)");
            str.AppendLine("globals[\"data\"].InstanceQuantity(\"test\")");
            str.AppendLine("globals[\"data\"].GetInstances(\"test\")");
            str.AppendLine("globals[\"data\"].AttributeAdd(\"test\", \"name\", \"string\")");
            str.AppendLine("globals[\"data\"].HasAttribute(\"test\", \"name\")");
            str.AppendLine("globals[\"data\"].AttributeRemove(\"test\", \"name\")");
            //str.AppendLine("globals[\"data\"].DataStoreSave(\"test\")");
            //str.AppendLine("globals[\"data\"].DataStoreUnload(\"test\")");
            //str.AppendLine("globals[\"data\"].DataStoreLoad(\"test\")");
            //str.AppendLine("globals[\"data\"].SaveState(\"testState\")");
            //str.AppendLine("globals[\"data\"].CreateAutosave");
            //str.AppendLine("globals[\"data\"].LoadState(\"testState\")");
            //str.AppendLine("globals[\"data\"].LoadAutosave(\"testState\")");            
            //str.AppendLine("globals[\"data\"].GetStates");
            str.AppendLine("globals[\"data\"].RemoveDataStore(\"test\")");
            #endregion

            //set the flag to test mode
            DataIntrinsics.testMode = true;

            //create Interpreter instance and assign global value
            Interpreter intp = new Interpreter(str.ToString());
            intp.errorOutput = ErrorOutput;
            intp.standardOutput = StandardOutput;            
            intp.Compile();
            intp.SetGlobalValue("data", DataIntrinsics.Get());
            intp.RunUntilDone();


            DataIntrinsics.testMode = false;
        }

        static void ErrorOutput(string msg) { Debug.Log("DataIntrinsicTest Error: " + msg); }

        static void StandardOutput(string msg) { Debug.Log("DataIntrinsicTest StdOutput: " + msg); }
    }
}

