# ELROWTP
![alt tag](http://sites.usa.gov/wp-content/uploads/2012/11/read-me.jpg)<br />
How to compile the position server:

The project uses Cmake. For instruction on how to use Cmake refer to:
http://www.cmake.org/documentation/

How to run the server:
Once positionserver is compiled start the generated .exe file from the command line.
It requires the following arguments:

$./positionserver.exe <camera input> <multicast ip> <debug>

The camera input corresponds to te id of the opened video capturing device (i.e. a camera index). 
If there is a single camera connected, just pass 0.

The multicast ip correspnds to the adress the client is listening to.

The debug keyword enables debug mode in which additional debug screens are opened (recommended). Simply type "debug" to enable.

