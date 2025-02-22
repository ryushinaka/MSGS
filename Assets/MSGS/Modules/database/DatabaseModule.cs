using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Database
{
    /// <summary>
    /// Provides the declaration and implementation of the database functionality exposed via a ValMap object in MiniScript
    /// </summary>
    /// <remarks>To acquire an object to access the in-memory database, simply call .Get()
    /// to acquire a unique instance for querying data.</remarks>
    public static class DatabaseModule
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, List<QueryCommand>> workingContext;

        public static bool testMode = false;

        static ValMap dbIntrinsics;

        static bool hasInitialized = false;
        public static bool HasInitialized
        {
            get { return hasInitialized; }
        }

        public static void Initialize()
        {
            //must make sure to initialize our tracking object
            workingContext = new Dictionary<string, List<QueryCommand>>();

            dbIntrinsics = new ValMap();
            dbIntrinsics.assignOverride = DisallowAssignment;

            //assign all the Intrinsic calls to the ValMap
            Database.Intrinsics.AvgQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.CountQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.LimitQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.MaxQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.MinQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.OrderByQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.SelectQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.SumQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.WhereIsQuery.Query(ref dbIntrinsics);
            Database.Intrinsics.WhereNotQuery.Query(ref dbIntrinsics);

            #region notes
            //var a = Intrinsic.Create("Join");
            //#region

            //#endregion

            //a = Intrinsic.Create("InnerJoin");
            //#region

            //#endregion

            //a = Intrinsic.Create("LeftJoin");
            //#region

            //#endregion

            //a = Intrinsic.Create("RightJoin");
            //#region

            //#endregion

            //a = Intrinsic.Create("FullJoin");
            //#region

            //#endregion

            //a = Intrinsic.Create("CrossJoin");
            //#region

            //#endregion

            //a = Intrinsic.Create("Union");
            //#region

            //#endregion

            //a = Intrinsic.Create("Intersect");
            //#region

            //#endregion

            //a = Intrinsic.Create("Distinct");
            //#region

            #endregion

            hasInitialized = true;
        }

        public static ValMap Get()
        {
            if (hasInitialized) return dbIntrinsics;

            return dbIntrinsics;
        }

        static bool DisallowAssignment(Value key, Value value)
        {
            // Assignment override function: return true to cancel (override)
            // the assignment, or false to allow it to happen as normal.
            throw new RuntimeException("DataIntrinsics: Assignment to protected map");
        }

        public static ValMap GetNew()
        {
            ValMap rst = new ValMap();
            //necessary since the ValMap does not implement ICloneable
            foreach (KeyValuePair<Value, Value> kv in dbIntrinsics.map)
            {   //copy the intrinsic function references to the new ValMap
                rst.map.Add(kv.Key, kv.Value);
            }

            //generate a unique GUID based tag for this module reference
            var tag = System.Guid.NewGuid().ToString();

            //add it to the ValMap instance
            rst.map.Add(new ValString("DBID"), new ValString(tag));

            //before we return the ValMap instance, register it with the database module
            //this allows the database module to have some "working context" for each query object
            //and allows each script to have multiple instances of queries
            workingContext.Add(tag, new List<QueryCommand>());

            return rst;
        }

        public static object EnqueueQuery(string id, QueryCommandType qcType, object mvalue)
        {
            //testMode gives us full UnityEngine.Debug.Log() output from each mechanism
            if (testMode)
            {

                
                return null;
            }


            //check the "state" of the list of QueryCommand elements
            //if the list is empty, then we're ready for a Select statement (logically, all db cmds start with Select at least once)
            //if the list is not empty and the QueryCommand is NOT 'End', append the qcType to the list + mvalue
            //if the QueryCommand is 'End', then we move to evaluate the sequence of QueryCommands

            if (workingContext.ContainsKey(id) && qcType == QueryCommandType.End) { return EndQuery(id); }
            
            return null;
        }

        private static object EndQuery(string id)
        {
            //here we evaluate the sequence of query commands & parameters
            //the goal here is to intelligently inform the user as best as possible from the query commands given
            //we can do better than 'syntax error 101 at '' on keyword select' lol

            return null;
        }
    }

    public struct QueryCommand
    {
        public QueryCommandType qType;
        public object value1, value2, value3, value4;
    }

    public enum QueryCommandType : int
    {
        Select,
        WhereIs,
        WhereNot,
        OrderBy,
        Limit,
        Sum,
        Avg,
        Count,
        Min,
        Max,

        AsList,
        AsMap,

        End,
    }
}
