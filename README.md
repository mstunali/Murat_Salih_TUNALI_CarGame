Unity version: 2020.3.23f1

Scriptable Object

Demo video: https://github.com/mstunali/Murat_Salih_TUNALI_CarGame/blob/main/Videos/CarScriptableObjectDemo.mov

CarData: There are two kinds of cars. The blue one is big and slow, the green one is small and faster. With this scriptable object you can choose the type of vehicle to be used in the game. You can also set the speed and rotation angles in the inspector.

Level Editor

Demo video: https://github.com/mstunali/Murat_Salih_TUNALI_CarGame/blob/main/Videos/LevelEditorDemo.mov

You can use the LevelEditor scene to design the levels. In this scene, there is an object called LevelEditor. When you click on this object, you can do the following in the inspector:

Level Index: This parameter allows you to choose which level to edit.

Wave Index: This parameter allows you to select the starting and ending point of the level to edit.

Open Current Wave: Loads the layout from the selected indexes to the scene. If the selected index has not been saved before, an error message will appear. You can open a previously saved layout.

Save Start-End For Wave: Writes the arranged start and end points to the file for the selected index. You can change the layout in a previously saved index, or you can save the new layout in the next index of the last saved index.

Save Obstacles For Level: Writes the edited obstacle objects to the file. Since the obstacles are the same for the whole level, the selected wave index is unimportant.

Destroy Start: Destroys the starting point object.

Create Start: Creates the starting point object.

Start Position: You set the position of the starting point.

Destroy End: Destroys the endpoint object.

Create End: Creates the endpoint object.

End Position: You set the position of the end point.

Clear All Obstaclees: Removes all obstacles in the scene.

Create Obstacle: Creates an Obstacle object. You can create with more than one object. You can set the positions of all of them separately.

Load Data From File: Allows you to reload the last saved levels to the file.

Remove Data From File: Deletes all the levels saved in the file.


Game

Car Behavior Base: This script is responsible for the car's movement and interaction with other objects. If it is a controlled vehicle, it acts according to the controls and records the points it passed. If it reaches the target, these points are used for the repetition of the movement. If it is not the controlled vehicle, it follows the saved points. It is an abstract class. All vehicle behavior scripts must implement this script. Derived classes may behave differently from this base class, but this main behavior is the same in all of them.

Game Manager: Calls the relevant functions from the car manager and level manager in case the game starts, is won or lost.

Level Manager: It is responsible for creating related objects by reading the data of the levels.

Car Manager: Responsible for the creation of car objects. The data of the cars to be replayed are also kept in this manager.

Level Data: Serialized classes that hold level information. Classes that are written to and read from the file as json.

