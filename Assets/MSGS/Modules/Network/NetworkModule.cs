using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Network
{
    public static class NetworkModule
    {
        internal static bool debug;
        static bool hasInitialized;
        static ValMap mapNetwork;

        public static ValMap Get()
        {
            if(!hasInitialized) { Initialize(); }

            

            return mapNetwork;
        }

        public static void Initialize()
        {
            mapNetwork = new ValMap();

            #region script functions for HTTP
            //RegisterRPC - string/FuncName + @funcRef - allows declaration of a RPC and the MiniScript code for it

            //UnregisterRPC - string/FuncName - tells the server to remove a particular RPC

            //CallRPC - string/FuncName

            //RPCList - returns full list of RPC's callable on the server

            //Ping - ping the server, returns full route latency as ValNumber
            #endregion

            hasInitialized = true;
        }
    }
}

