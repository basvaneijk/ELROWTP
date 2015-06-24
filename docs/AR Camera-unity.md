# Using AR camera in Unity
 
## GUIDE to have main camera of mobile as background:

* Step 1: Drag the "Background Camera 1" prefab located in the directory assets/framework to the scene.
* Step 2: edit MainCamera object with this settings:
		Clear Flags 	: Depth only
		Culling Mask	: mixed...    (everything except CameraSphere(layer)).
		Depth		: 1
 
## How it works:
A webcamtexture in the scene takes the input of a camera/webcam. 
That texture is displayed on a plane, which is captured by a unity camera with a orthographic projection.
