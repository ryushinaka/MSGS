using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Testing
{
    public static class MasterUnitTest
    {
        public static void Run()
        {
            //first we have to create a junk/filler Game Modification for the Test to execute against            
            //GameModification gc = GameModification.GetTestModification();
            
            //ModManagementSingleton.Instance.CurrentMod = gc;
            

            //begin the first stage of testing, which is the Data Intrinsics object model
            Debug.Log("Beginning Full Unit Test of MSGS:\nFirst Test is the Data Module");
            DataIntrinsicsTest.DoTest();
        }
    }
}

