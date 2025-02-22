using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Scripts;
using MiniScript.MSGS.Extensions;

namespace MiniScript.MSGS.Data
{
    public static class DataIntrinsics
    {
        public static bool testMode = false;

        static bool hasInitialized = false;
        public static bool HasInitialized
        {
            get { return hasInitialized; }
        }

        static ValMap dataIntrinsics;
        static ValMap mapScriptableObjects;
        /// <summary>
        /// Create the ValMap of "anonymous" functions (funcRef's) with associated Key values
        /// </summary>
        public static void Initialize()
        {
            #region DataStoreWarehouse intrinsics
            dataIntrinsics = new ValMap();
            dataIntrinsics.assignOverride = DisallowAllAssignment;

            var a = Intrinsic.Create("");
            #region CreateDataStore
            a.AddParam("typename", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("CreateDataStore: " + context.GetLocalString("typename")); }

                DataStoreWarehouse.DataStoreCreate(context.GetLocalString("typename"));

                return new Intrinsic.Result(null, true);
            };

            IntrinsicsHelpMetadata.Create("CreateDataStore",
                "Creates a new storage for the declared Type.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name="typename", variableType = typeof(string),
                    Comment = "The name of the Type in storage"
                    }},
                null);

            dataIntrinsics.map.Add(TempValString.Get("CreateDataStore"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region RemoveDataStore
            a.AddParam("typename", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("RemoveDataStore: " + context.GetLocalString("typename")); }

                DataStoreWarehouse.DataStoreUnload(context.GetLocalString("typename"), false);

                return new Intrinsic.Result(null, true);
            };

            IntrinsicsHelpMetadata.Create("RemoveDataStore",
                "Removes the storage for the declared Type. This will remove the storage for the Type from memory as well as any saved instances.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "typename", variableType = typeof(string),
                    Comment = "The name of the Type in storage"
                    }
                }, null);
            dataIntrinsics.map.Add(TempValString.Get("RemoveDataStore"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetTypeStoreList
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("GetTypeStoreList"); }

                var l = DataStoreWarehouse.DataStoreList().ToValList();

                return new Intrinsic.Result(l, true);
            };

            IntrinsicsHelpMetadata.Create("GetTypeStoreList",
                "Returns the ValList containing ValString elements of each Type in storage.",
                "Data",
                new List<IntrinsicParameter>() { }, new IntrinsicParameter()
                {
                    Name = "",
                    variableType = typeof(ValList),
                    Comment = "A ValList containing ValMap elements representing each instance of the Type in storage."
                });
            dataIntrinsics.map.Add(TempValString.Get("GetTypeStoreList"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region Select
            a.AddParam("type", string.Empty);
            a.AddParam("property", string.Empty);
            a.AddParam("value", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    Debug.Log("Select: " + context.GetLocalString("type") + " " +
                        context.GetLocalString("property") + " " +
                        context.GetLocalString("value"));
                }

                var vl = DataStoreWarehouse.Select(context.GetLocalString("type"),
                    context.GetLocalString("property"),
                    context.GetLocalString("value"));

                return new Intrinsic.Result(vl);
            };

            dataIntrinsics.map.Add(TempValString.Get("Select"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Select",
                "Returns the ValList containing ValString elements of each Type in storage.",
                "Data",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { }
                },
                new IntrinsicParameter()
                {
                    Name = "",
                    variableType = typeof(ValList),
                    Comment = "A ValList containing ValMap elements representing each instance of the Type in storage."
                });
            #endregion

            a = Intrinsic.Create("");
            #region SelectRegx
            a.AddParam("type", "");
            a.AddParam("property", "");
            a.AddParam("pattern", "");
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    Debug.Log("SelectRegx: " + context.GetLocalString("type") + " " + context.GetLocalString("property") + " " +
        context.GetLocalString("pattern"));
                }
                throw new System.NotImplementedException("DataIntrinsics.SelectRegx");

                //return new Intrinsic.Result(null);
            };

            dataIntrinsics.map.Add(TempValString.Get("SelectRegx"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SelectRegx",
                "Returns the ValList containing ValString elements of each Type in storage.",
                "Data",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { }
                },
                new IntrinsicParameter()
                {
                    Name = "",
                    variableType = typeof(ValList),
                    Comment = "A ValList containing ValMap elements representing each instance of the Type in storage."
                });
            #endregion

            a = Intrinsic.Create("");
            #region SelectRange
            a.AddParam("type", string.Empty);
            a.AddParam("property", string.Empty);
            a.AddParam("lower");
            a.AddParam("upper");
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    Debug.Log("SelectRange: " + context.GetLocalString("type") + " " + context.GetLocalString("property") + " " +
        context.GetLocalString("lower") + " " + context.GetLocalString("upper"));
                }
                throw new System.NotImplementedException("DataIntrinsics.SelectRange");

                //return new Intrinsic.Result(null);
            };

            dataIntrinsics.map.Add(TempValString.Get("SelectRange"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SelectRange",
                "Returns the ValList containing ValMap elements of each Type in storage.",
                "Data",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { },
                    new IntrinsicParameter() { }
                },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(ValList),
                    Comment = "A ValList containing ValMap elements representing each instance of the Type in storage."
                });
            #endregion

            a = Intrinsic.Create("");
            #region GetRandomInstance
            a.AddParam("type", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("GetRandomInstance: " + context.GetLocalString("type")); }

                var rf = DataStoreWarehouse.DataStoreGetRandomInstance(context.GetLocalString("type"));
                if (rf != null) { return new Intrinsic.Result(rf); }
                else { return new Intrinsic.Result(null, true); }
            };

            IntrinsicsHelpMetadata.Create("GetRandomInstance",
                "Gets one of a randomly selected instance of the specified Type from storage.",
                "Data",
               new List<IntrinsicParameter>() {
                   new IntrinsicParameter() { Name = "type", variableType = typeof(string),
                   Comment = "The name of the Type in storage" }
               }, null);

            dataIntrinsics.map.Add(TempValString.Get("GetRandomInstance"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetRandomInstances
            a.AddParam("type", "");
            a.AddParam("quantity", 0);
            a.AddParam("unique", 0);
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    Debug.Log("GetRandomInstances: " + context.GetLocalString("type") + " " + context.GetLocalString("quantity") + " " +
        context.GetLocalString("unique"));
                }

                if (DataStoreWarehouse.Contains(context.GetLocalString("type")))
                {
                    ValNumber za = (ValNumber)context.GetLocal("quantity");
                    ValNumber zb = (ValNumber)context.GetLocal("unique");

                    return new Intrinsic.Result(DataStoreWarehouse.DataStoreGetRandomInstances(
                        context.GetLocalString("type"), za.IntValue(), zb.BoolValue()));
                }

                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("GetRandomInstance",
                "Gets the 'quantity' of a randomly selected instance of the specified Type from storage, while 'unique' optionally makes them non-repeating when set to true.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment= "The name of the Type in storage." },
                    new IntrinsicParameter() { Name = "quantity", variableType = typeof(int), Comment = "The quantity of instances to randomly select of the Type from storage"},
                    new IntrinsicParameter() { Name = "unique", variableType = typeof(bool), Comment = "If true, each instance selected from the Types storage will be unique/non-repeating."}
                },
                null);

            dataIntrinsics.map.Add(TempValString.Get("GetRandomInstances"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetInstances
            a.AddParam("type", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("GetInstances: " + context.GetLocalString("type")); }

                return new Intrinsic.Result(DataStoreWarehouse.DataStoreGetInstances(context.GetLocalString("type")));
            };

            IntrinsicsHelpMetadata.Create("GetInstances",
                "Returns the ValList containing the ValMap object of each instance for the specified Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() {
                        Name = "type",
                        variableType = typeof(string),
                        Comment = "The name of the Type in storage." } },
                new IntrinsicParameter()
                {
                    Name = "",
                    variableType = typeof(ValList),
                    Comment = "The ValList containing the ValMap object of each instance"
                });

            dataIntrinsics.map.Add(TempValString.Get("GetInstances"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetInstanceAtIndex
            a.AddParam("type", "");
            a.AddParam("index", 0);
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("GetInstanceAtIndex: " + context.GetLocalString("type")); }

                return new Intrinsic.Result(DataStoreWarehouse.DataStoreGetInstanceAtIndex(context.GetLocalString("type"),
                    context.GetLocalInt("index")));
            };

            IntrinsicsHelpMetadata.Create("GetInstanceAtIndex",
                "Returns the ValMap object of the instance at the specified Index for the specified Type in storage.",
                "Data",
                new List<IntrinsicParameter>() { new IntrinsicParameter() { Name = "type", variableType = typeof(string),
                Comment = "The name of the Type in storage." } },
                new IntrinsicParameter() { Name = "", variableType = typeof(ValMap), Comment = "The ValMap object of the instance" });

            dataIntrinsics.map.Add(TempValString.Get("GetInstanceAtIndex"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region RemoveInstance
            a.AddParam("type", string.Empty);
            a.AddParam("map", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("RemoveInstance: " + context.GetLocalString("type") + " " + context.GetLocalString("map")); }

                DataStoreWarehouse.DataStoreDestroyInstance(context.GetLocalString("type"), context.GetLocal("map") as ValMap);

                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("RemoveInstance",
                "Removes the instance from the storage for its Type.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                    new IntrinsicParameter() { Name = "map", variableType = typeof(ValMap), Comment = "The ValMap object of the instance of the Type" }
                },
                null);

            dataIntrinsics.map.Add(TempValString.Get("RemoveInstance"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region  RemoveInstances
            a.AddParam("type", "");
            a.AddParam("list", new ValList());
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    if (context.GetLocal("list") == null) { Debug.Log("RemoveInstances: " + context.GetLocalString("type") + " " + 0); }
                    else { Debug.Log("RemoveInstances: " + context.GetLocalString("type") + " " + ((ValList)context.GetLocal("list")).values.ToString()); }
                }

                if (context.GetLocal("list") is ValList)
                {
                    DataStoreWarehouse.DataStoreDestroyInstances(
                        context.GetLocalString("type"),
                        context.GetLocal("list") as ValList);
                    return new Intrinsic.Result(ValNumber.Truth(true));
                }

                return new Intrinsic.Result(ValNumber.Truth(false));
            };

            IntrinsicsHelpMetadata.Create("RemoveInstances",
                "Removes the instances from the storage for its Type.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                    new IntrinsicParameter() { Name = "list", variableType = typeof(ValList), Comment = "The ValList object containing the instances of the Type" }
                }, null);

            dataIntrinsics.map.Add(TempValString.Get("RemoveInstances"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region UpdateInstance
            a.AddParam("type", string.Empty);
            a.AddParam("vm", new ValMap());
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("UpdateInstance: " + context.GetLocalString("type")); }

                if (DataStoreWarehouse.Contains(context.GetLocalString("type")))
                {
                    var x = context.GetLocal("vm") as ValMap;
                    if (x != null)
                    {
                        if (x.map.Keys.Count > 0)
                        {
                            DataStoreWarehouse.UpdateInstance(context.GetLocalString("type"), ref x);
                        }
                    }
                }

                return new Intrinsic.Result(null, true);
            };

            dataIntrinsics.map.Add(TempValString.Get("UpdateInstance"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("UpdateInstance",
                "Updates the instance in the backend data storage, should be called after changes are made to the instance.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                    new IntrinsicParameter() { Name = "vm", variableType = typeof(ValMap), Comment = "The ValMap object containing the instance to update its source from." }
                }, null);
            #endregion

            a = Intrinsic.Create("");
            #region CreateInstance
            a.AddParam("type", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("CreateInstance: " + context.GetLocalString("type")); }

                ValMap vm = null;
                DataStoreWarehouse.DataStoreCreateInstance(context.GetLocalString("type"), out vm);
                return new Intrinsic.Result(vm);
            };

            IntrinsicsHelpMetadata.Create("CreateInstance",
                "Creates an instance of the Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                }, null);

            dataIntrinsics.map.Add(TempValString.Get("CreateInstance"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region CreateInstances
            a.AddParam("type", "");
            a.AddParam("quantity", 0);
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("CreateInstances: " + context.GetLocalString("type") + " " + context.GetLocalInt("quantity")); }

                DataStoreWarehouse.DataStoreCreateInstances(
                    context.GetLocalString("type"),
                    context.GetLocalInt("quantity"), out ValList rst);
                Debug.Log(rst.ToString());
                return new Intrinsic.Result(rst);
            };

            IntrinsicsHelpMetadata.Create("CreateInstances",
                "Creates an instance of the Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                    new IntrinsicParameter() { Name = "quantity", variableType = typeof(int), Comment = "The quantity of instances to create" }
                }, null);

            dataIntrinsics.map.Add(TempValString.Get("CreateInstances"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region InstanceQuantity
            a.AddParam("type", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("InstanceQuantity"); }

                ValNumber qty;
                DataStoreWarehouse.DataStoreInstanceCount(context.GetLocalString("type"), out qty);

                return new Intrinsic.Result(qty);
            };

            IntrinsicsHelpMetadata.Create("InstanceQuantity",
                "Returns the quantity of instances for the Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                }, new IntrinsicParameter() { Name = "", variableType = typeof(int), Comment = "An integer value that is the quantity of instances." });

            dataIntrinsics.map.Add(TempValString.Get("InstanceQuantity"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region HasAttribute
            a.AddParam("type", "");
            a.AddParam("propertyname", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("HasAttribute: " + context.GetLocalString("type") + " " + context.GetLocalString("propertyname")); }

                ValNumber tf;
                DataStoreWarehouse.DataStoreHasAttribute(context.GetLocalString("type"), context.GetLocalString("propertyname"), out tf);
                return new Intrinsic.Result(tf, true);
            };

            IntrinsicsHelpMetadata.Create("HasAttribute",
                "Returns true if the Type has the attribute specified.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage." },
                },
                new IntrinsicParameter() { Name = "", variableType = typeof(bool), Comment = "An boolean value" });

            dataIntrinsics.map.Add(new ValString("HasAttribute"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region AttributeAdd
            a.AddParam("type", string.Empty);
            a.AddParam("propertyname", string.Empty);
            a.AddParam("propertytype", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (testMode)
                {
                    Debug.Log("AddAttribute: " + context.GetLocalString("type") + " " + context.GetLocalString("propertyname") + " " +
                        context.GetLocal("propertytype").ToString());
                }

                ValNumber tf;
                DataStoreWarehouse.DataStoreAddAttribute(
                    context.GetLocalString("type"),
                    context.GetLocalString("propertyname"),
                    context.GetLocal("propertytype"),
                    out tf);
                return new Intrinsic.Result(tf);
            };

            dataIntrinsics.map.Add(TempValString.Get("AttributeAdd"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("AttributeAdd",
                "Adds the attribute to the Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage to add the attribute to." },
                    new IntrinsicParameter() { Name = "propertyname", variableType = typeof(string), Comment = "The label of the attribute to add to the Type." },
                    new IntrinsicParameter() { Name = "propertytype", variableType = typeof(string), Comment = "The type of attribute to add to the Type, string or number are permitted." }
                },
                new IntrinsicParameter() { Name = "", variableType = typeof(bool), Comment = "Returns true if successful, false otherwise." });
            #endregion

            a = Intrinsic.Create("");
            #region AttributeRemove
            a.AddParam("type", "");
            a.AddParam("propertyname", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("RemoveAttribute: " + context.GetLocalString("type") + " " + context.GetLocalString("propertyname")); }

                ValNumber tf;
                DataStoreWarehouse.DataStoreRemoveAttribute(
                    context.GetLocalString("type"),
                    context.GetLocalString("propertyname"),
                    out tf);
                return new Intrinsic.Result(tf);
            };

            IntrinsicsHelpMetadata.Create("AttributeRemove",
                "Removes the attribute from the Type in storage.",
                "Data",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage to remove the attribute from." },
                    new IntrinsicParameter() { Name = "propertyname", variableType = typeof(string), Comment = "The label of the attribute to remove from the Type." },
                },
                new IntrinsicParameter() { Name = "", variableType = typeof(bool), Comment = "Returns true if successful, false otherwise." });

            dataIntrinsics.map.Add(TempValString.Get("AttributeRemove"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region DataStoreSave
            a.AddParam("type", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("DataStoreSave: " + context.GetLocalString("type")); }

                DataStoreWarehouse.DataStoreSave(context.GetLocalString("type"));

                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("DataStoreSave",
               "Saves the Type collection in storage to file, optionally unloads/removes the Type from memory after saving.",
               "Data",
               new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage to save." },
                    new IntrinsicParameter() { Name = "unload", variableType = typeof(bool), Comment = "If true, removes the Type from memory after saving it to file." },
               },
               null);

            dataIntrinsics.map.Add(TempValString.Get("DataStoreSave"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region DataStoreLoad
            a.AddParam("type", "");
            a.AddParam("forceReplace", 0); //default to false
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("LoadDataStore: " + context.GetLocalString("type")); }

                DataStoreWarehouse.DataStoreLoad(
                    context.GetLocalString("type"),
                    context.GetLocalBool("forceReplace"));
                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("DataStoreLoad",
               "Loads the Type collection in storage to memory, optionally replacing the existing Type collection contents.",
               "Data",
               new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage to load." },
                    new IntrinsicParameter() { Name = "forceReplace", variableType = typeof(bool), Comment = "If true, replaces the Type collection in memory after loading it from file." },
               },
               null);

            dataIntrinsics.map.Add(TempValString.Get("DataStoreLoad"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region DataStoreUnload
            a.AddParam("type", "");
            a.AddParam("saveFirst", 0); //default to false
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("UnloadDataStore: " + context.GetLocalString("type")); }

                DataStoreWarehouse.DataStoreUnload(
                    context.GetLocalString("type"),
                    context.GetLocalBool("saveFirst"));

                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("DataStoreUnload",
               "Unloads the Type collection in storage to memory, optionally saving the Type collection to file.",
               "Data",
               new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "type", variableType = typeof(string), Comment = "The name of the Type in storage to unload." },
                    new IntrinsicParameter() { Name = "saveFirst", variableType = typeof(bool), Comment = "If true, saves the Type collection in memory to file." },
               },
               null);

            dataIntrinsics.map.Add(TempValString.Get("DataStoreUnload"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region SaveState
            a.AddParam("savename", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("SaveState: " + context.GetLocalString("savename")); }

                DataStoreWarehouse.StateSave(
                    context.GetLocalString("savename"));
                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("SaveState",
               "Saves all currently in memory Type collections (and their instances) to a file.",
               "Data",
               new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "savename", variableType = typeof(string), Comment = "The name to be assigned to the State for reference." }
               },
               null);

            dataIntrinsics.map.Add(TempValString.Get("SaveState"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region CreateAutosave
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("CreateAutosave"); }

                DataStoreWarehouse.StateAutoSave();
                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("CreateAutosave",
               "Saves all currently in memory Type collections (and their instances) to a file. There is only ever one \"quicksave\" in storage.",
               "Data",
               new List<IntrinsicParameter>() { },
               null);

            dataIntrinsics.map.Add(TempValString.Get("CreateAutosave"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region LoadState
            a.AddParam("savename", "");
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("LoadState: " + context.GetLocalString("savename")); }

                DataStoreWarehouse.StateLoad(context.GetLocalString("savename"));
                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("LoadState",
               "Loads the state into memory from file, restoring all Type collections and their instances.",
               "Data",
               new List<IntrinsicParameter>() {
                   new IntrinsicParameter() { Name = "savename", variableType = typeof(string),
                       Comment = "The name of the state to label it with." }
               },
               null);

            dataIntrinsics.map.Add(TempValString.Get("LoadState"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region LoadAutosave
            a.code = (context, partialResult) =>
            {
                if (testMode) { Debug.Log("LoadAutosave"); }

                DataStoreWarehouse.StateLoad("autosave");
                return new Intrinsic.Result(ValNumber.Truth(false));
            };

            IntrinsicsHelpMetadata.Create("LoadState",
               "Loads the state stored in the \"autosave\" location into memory, restoring all Type collections and their instances.",
               "Data",
               new List<IntrinsicParameter>() { }, null);

            dataIntrinsics.map.Add(TempValString.Get("LoadAutosave"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetStates
            a.code = (context, partialResult) =>
            {
                var vl = DataStoreWarehouse.GetStates();

                if (testMode)
                {
                    string rst = string.Empty;
                    ValString vref;
                    foreach (Value v in vl.values)
                    {
                        vref = v as ValString;
                        rst += vref.value + ",";
                    }
                    Debug.Log("GetStates: " + rst);
                }

                return new Intrinsic.Result(DataStoreWarehouse.GetStates());
            };

            IntrinsicsHelpMetadata.Create("GetStates",
               "Returns a ValList containing ValString elements for each State stored in file.",
               "Data",
               new List<IntrinsicParameter>() { },
               new IntrinsicParameter() { variableType = typeof(ValList), Comment = "The ValList contains ValString elements for the names of each State available." });

            dataIntrinsics.map.Add(TempValString.Get("GetStates"), a.GetFunc());
            #endregion
            #endregion

            hasInitialized = true;
        }

        /// <summary>
        /// Generic callback to disallow assignment of any ValMap created by this module
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static bool DisallowAllAssignment(Value key, Value value)
        {
            throw new RuntimeException("DataIntrinsics: Assignment to protected map");
        }

        public static ValMap Get()
        {
            if (dataIntrinsics == null) { Initialize(); }
            return dataIntrinsics;
        }

        public static string GetDebugScriptSource()
        {
            return  //make 'pp' the short symbol for the PrettyPrint function
                "pp = @globals[\"Host\"].PrettyPrint \r\n" +
                //make 'ds' the short symbol for the Data intrinsic functions
                "ds = @globals[\"Data\"] \r\n" +
                //make 'log' the short symbol for the LogInfo function
                "log = @globals[\"Host\"].LogInfo \r\n" +

                "ds.CreateDataStore(\"Character\") \r\n" +

                "ds.AttributeAdd(\"Character\", \"name\", \"string\") \r\n" +
                "ds.AttributeAdd(\"Character\", \"age\", \"number\") \r\n" +
                "ds.AttributeAdd(\"Character\", \"race\", \"string\") \r\n" +
                "ds.AttributeAdd(\"Character\", \"class\", \"string\") \r\n" +
                "ds.AttributeAdd(\"Character\", \"level\", \"number\") \r\n" +
                "ds.AttributeAdd(\"Character\", \"XP\", \"number\") \r\n" +

                "myChar = ds.CreateInstance(\"Character\") \r\n" +
                "myChar[\"name\"] = \"aRandomName\" \r\n" +
                "myChar[\"race\"] = \"Human\" \r\n" +
                "myChar[\"age\"] = 21 \r\n" +
                "myChar[\"level\"] = 4 \r\n" +
                "myChar[\"class\"] = \"Fighter\" \r\n" +

                "ds.UpdateInstance(\"Character\", myChar) \r\n" +
                "log(pp(myChar)) \r\n" +

                "myChar = ds.CreateInstance(\"Character\") \r\n" +
                "myChar[\"name\"] = \"SimpleMage\" \r\n" +
                "myChar[\"race\"] = \"Human\" \r\n" +
                "myChar[\"age\"] = 18 \r\n" +
                "myChar[\"level\"] = 6 \r\n" +
                "myChar[\"class\"] = \"Mage\" \r\n" +

                "ds.UpdateInstance(\"Character\", myChar) \r\n" +
                "log(pp(myChar)) \r\n" +

                "characters = ds.GetInstances(\"Character\") \r\n" +
                "log(\"The complete list of all Character records in data storage: \" + pp(characters)) \r\n" +

                "log(\"How many Character records are there in the data storage?\") \r\n" +
                "log(\"Character Count= \" + ds.InstanceQuantity(\"Character\")) \r\n" +

                "log(\"Select a specific Character by name from the data storage.\") \r\n" +
                "aChar = ds.Select(\"Character\", \"name\", \"aRandomName\") \r\n" +
                "log(\"Selected Character: \" + pp(aChar)) \r\n" +

                //create a storage for weapons
                "ds.CreateDataStore(\"Weapons\") \r\n" +
                "ds.AttributeAdd(\"Weapons\", \"name\", \"string\") \r\n" +
                "ds.AttributeAdd(\"Weapons\", \"weight\", \"number\") \r\n" +
                "ds.AttributeAdd(\"Weapons\", \"d6\", \"number\") \r\n" + //how many d6 to roll
                "ds.AttributeAdd(\"Weapons\", \"diemodifier\", \"number\") \r\n" + //any +1 or +2 modifier to the roll
                "ds.AttributeAdd(\"Weapons\", \"extra\", \"string\") \r\n" +

                //create 3 Weapon records, returned as a ValList of ValMap elements
                "weapons = ds.CreateInstances(\"Weapons\", 3) \r\n" +
                "weapons[0][\"name\"] = \"Broad sword\" \r\n" +
                "weapons[1][\"name\"] = \"Long sword\" \r\n" +
                "weapons[2][\"name\"] = \"Short sword\" \r\n" +
                //update the data storage for each record to have the new name value
                "ds.UpdateInstance(\"Weapons\", weapons[0]) \r\n" +
                "ds.UpdateInstance(\"Weapons\", weapons[1]) \r\n" +
                "ds.UpdateInstance(\"Weapons\", weapons[2]) \r\n" +

                //create a storage for armor
                "ds.CreateDataStore(\"Armor\") \r\n" +
                "ds.AttributeAdd(\"Armor\", \"name\", \"string\") \r\n" +
                "ds.AttributeAdd(\"Armor\", \"weight\", \"number\") \r\n" +
                "ds.AttributeAdd(\"Armor\", \"AC\", \"number\") \r\n" + //how much to modify to-hit die rolls
                "ds.AttributeAdd(\"Armor\", \"extra\", \"string\") \r\n" +

                //create 3 armor records
                "armors = ds.CreateInstances(\"Armor\", 3) \r\n" +
                "armors[0][\"name\"] = \"Leather Armor\" \r\n" +
                "armors[0][\"weight\"] = 2 \r\n" +
                "armors[0][\"AC\"] = 1 \r\n" +
                "armors[1][\"name\"] = \"Plate Armor\" \r\n" +
                "armors[1][\"weight\"] = 5 \r\n" +
                "armors[1][\"AC\"] = 4 \r\n" +
                "armors[2][\"name\"] = \"Scale Armor\" \r\n" +
                "armors[2][\"weight\"] = 7 \r\n" +
                "armors[2][\"AC\"] = 3 \r\n" +
                "ds.UpdateInstance(\"Armor\", armors[0]) \r\n" +
                "ds.UpdateInstance(\"Armor\", armors[1]) \r\n" +
                "ds.UpdateInstance(\"Armor\", armors[2]) \r\n" +

                "types = ds.GetTypeStoreList \r\n" +
                "log(\"What record types are in storage: \" + pp(types)) \r\n" +

                //remove the 'extra' property from the armor record and weapon record
                "ds.AttributeRemove(\"Weapons\", \"extra\") \r\n" +
                //and from the Armor record
                "ds.AttributeRemove(\"Armor\", \"extra\") \r\n" +
                //it should be noted that calls to AttributeRemove does NOT modify any existing references stored
                //locally in the script, to update the local references GetInstances or Select will have to be used
                //to repopulate the local variables

                //to facilitate loot tables or similar, GetRandomInstance and GetRandomInstances is provided
                //"ds.GetRandomInstance(\"Weapons\") \r\n" + //returns a single ValMap of the randomly chosen Weapon record
                //"ds.GetRandomInstances(\"Weapons\", 10) \r\n" + //returns a ValList containing ValMap elements of the 10 Weapon records chosen

                "ds.RemoveInstance(\"Character\", myChar) \r\n" +
                //removes the myChar character record from data storage
                //the local script will continue to have the local reference (myChar) until the variable is reassigned or set to null

                "ds.RemoveInstances(\"Armor\", armors) \r\n" +
                //removes all the Armor records from data storage
                //the local script will continue to have the local reference (armors) until the variable is reassigned or set to null

                "ds.RemoveDataStore(\"Weapons\") \r\n" +
                //removes the Weapon record from data storage and all its entries
                //CreateDataStore & RemoveDataStore let you create and destroy record types as you want to, for temporary or permanent storage.
                //the local script will continue to have the local reference (weapons) until the variable is reassigned or set to null

                "log \"Now that the Weapons records have been removed, how many types are left in data storage?\" \r\n" +
                "log(pp(ds.GetTypeStoreList)) \r\n" +

                "log \"Test finished.\" ";
        }
    }
}


