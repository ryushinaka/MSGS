# CreamSoda
 Miniscript integration suite with Unity3D.
 
 Supported features:
 a) Define/Instance/Modify/Edit Type(s) via Intrinsic calls
 b) Management of Type instances in a database-like manner (xml backed DataSet in file format)
    i) each Type can be added/removed from memory as desired at runtime
	ii) each Type can be edited while it is in memory, adding or removing fields as desired
	iii) each Type can be instanced as much as desired, but memory constraints will need to be monitored
 c) UI System that wraps around basic UI controls (Legacy UI, not the current UI Toolkit controls in Unity)
	i) Support with Prefabs for Common Controls (button, image, listview, etc)
	ii) UI can be prefabed in Unity as you would normally, or you can use the provided script to "export" the hierarchy to file which can be loaded/unloaded at runtime as desired.
	
 Unsupported features:
 a) Networking:  its on the list of things I would like to see added, but its going to be a while.
 b) Data-binding of some kind for the UI controls.  Unimplemented at this time.

 This is very much a WIP and I am only one person, so expect there to be bugs and incomplete code as I work through this.
