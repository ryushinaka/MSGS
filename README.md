# CreamSoda
 Miniscript integration suite with Unity3D.
 
 Supported features:<br>
    _ a) Define/Instance/Modify/Edit Type(s) via Intrinsic calls<br>
    _ b) Management of Type instances in a database-like manner (xml backed DataSet in file format)<br>
         _ i) each Type can be added/removed from memory as desired at runtime<br>
	 _ ii) each Type can be edited while it is in memory, adding or removing fields as desired<br>
	 _ iii) each Type can be instanced as much as desired, but memory constraints will need to be monitored<br>
    _ c) UI System that wraps around basic UI controls (Legacy UI, not the current UI Toolkit controls in Unity)<br>
	 _ i) Support with Prefabs for Common Controls (button, image, listview, etc)<br>
	 _ ii) UI can be prefabed in Unity as you would normally, or you can use the provided script to "export" the hierarchy to file which can be loaded/unloaded at runtime as desired.<br>
	
 Unsupported features:<br>
    _ a) Networking:  its on the list of things I would like to see added, but its going to be a while.<br>
    _ b) Data-binding of some kind for the UI controls.  Unimplemented at this time.<br>

 This is very much a WIP and I am only one person, so expect there to be bugs and incomplete code as I work through this.
