# Using AR camera in Unity
 
## GUIDE to have main camera of mobile as background:

* Step 1: In folder assets/framework is a prefab Background Camera 1.
* Step 2: drag the prefab in your project.
* Step 3: edit MainCamera object with this settings:
		Clear Flags 	: Depth only
		Culling Mask	: mixed...    (everything except CameraSphere(layer)).
		Depth		: 1
* Step 4: Save & run unity.
 
## How its working:
In Unity there is a webcamtexture that takes the input of a camera/webcam. 
That is displayed on a plane. the plane is captured by a unity camera with a orthographic projection.
