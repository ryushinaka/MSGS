/*

-----------------------------------------Toooooooooooooooo  Dooooooooooooooo -----------------------------------------------------------------------

1) DataStoreWarehouse.cs / DataIntrinsics.cs - Update the code to use the proper folder hierarchy.  Since the previous data model assumed that each package (zip)
would have a unique GUID identifier associated with it, this will need to be merged with the new format.  A quick fix would be to add a GUID declaration when
"Export Package" is performed, but if a user is updating/editing an existing package, this would invalidate existing local copies for other users, so add a GUID
as string field to the Export Package script/function in order to allow replication without losing prior saves/savestates for a package already present with a user.
An argument could be made that this is preferred behaviour, that each new iteration of script/package should have its own set of data, but it is my opinion that
users and their scripts/data/etc are not software and should not be treated as git tasks.

2) IntrinsicsHelpMetaData.cs - this should have functionality to make lookup and export possible from the command console.
perhaps through the "Host" module would make the most sense? But also asking each module specifically for its Intrinsic list would be useful in some cases.


-------------------------------WISH LIST -----------------------------------------------
---------Things I would like to add ---------------------------
)) ScriptExecutionContext - AutoRestart needs implementation
)) ScriptExecutionContext - Update/refresh the Globals passed to the script at the end of each script VM execution
)) ScriptExecutionContext - In some instances of script execution, scheduling the script to execute as part or whole of a Unity Job task
would be preferred to the alternative of regular thread scheduling. 
//https://docs.unity3d.com/Manual/job-system-custom-nativecontainer-example.html - this needs a new version written to this spec

*/