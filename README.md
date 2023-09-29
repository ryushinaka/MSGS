# CreamSoda
 Miniscript integration suite with Unity3D.
 
 Supported features:<br>
     a) Define/Instance/Modify/Edit Type(s) via Intrinsic calls<br>
     b) Management of Type instances in a database-like manner (xml backed DataSet in file format)<br>
          i) each Type can be added/removed from memory as desired at runtime<br>
	  ii) each Type can be edited while it is in memory, adding or removing fields as desired<br>
	  iii) each Type can be instanced as much as desired, but memory constraints will need to be monitored<br>
     c) UI System that wraps around basic UI controls (Legacy UI, not the current UI Toolkit controls in Unity)<br>
	  i) Support with Prefabs for Common Controls (button, image, listview, etc)<br>
	  ii) UI can be prefabed in Unity as you would normally, or you can use the provided script to "export" the hierarchy to file which can be loaded/unloaded at runtime as desired.<br>
     d) MiniScript script extra features:<br>
          i) Can prepend/postpend script source from/with any other script source that has been loaded into memory, changeable when script is not being executed.<br>
	  ii) Can schedule scripts to run on dedicated threads, if desired. (partially implemented)  This avoids Unity "main thread" limitations, where applicable<br>
   	  iii) Per Script, can register callbacks for state of the script execution (partially to not imlemented)<br>
	     
 Unsupported features:<br>
     a) Networking:  its on the list of things I would like to see added, but its going to be a while.<br>
     b) Data-binding of some kind for the UI controls.  Unimplemented at this time.<br>

 This is very much a WIP and I am only one person, so expect there to be bugs and incomplete code as I work through this.
