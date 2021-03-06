# ELROWTP README

For full server documentation check http://basvaneijk.github.io/ELROWTP/index.html <br />
For full launcher documentation check http://basvaneijk.github.io/ELROWTP/launcher/index.html <br />

       ________  _____     _______      ___     ____      ____  _________  _______   
      |_   __  ||_   _|   |_   __ \   .'   `.  |_  _|    |_  _||  _   _  ||_   __ \  
        | |_ \_|  | |       | |__) | /  .-.  \   \ \  /\  / /  |_/ | | \_|  | |__) | 
        |  _| _   | |   _   |  __ /  | |   | |    \ \/  \/ /       | |      |  ___/  
       _| |__/ | _| |__/ | _| |  \ \_\  `-'  /     \  /\  /       _| |_    _| |_     
      |________||________||____| |___|`.___.'       \/  \/       |_____|  |_____|    
                                                                               

The hardware we have used for testing is the following:<br />
- Microsoft lifecam cinema
- Linksys wag325n router
- Macbook Pro 15" retina 2012 (as server)
- Several different windows laptops (as server)

It should be noted that any of the hardware we have used can be replaced. Any modern router that supports multicast should work.

**The project exists out of 2 parts:**<br />
1. A Position server<br />
2. A Game client

In order to play the games on the game client the position server has to run and multicast to an adress the
client can listen to.

**How to compile the position server:**

The project uses Cmake. For instruction on how to use Cmake refer to:
http://www.cmake.org/documentation/

**How to run the server:**

Once positionserver is compiled start the generated .exe file from the command line.
It requires the following arguments:

$./positionserver.exe [camera input] [multicast ip] [debug]

The camera input corresponds to the id of the opened video capturing device (i.e. a camera index). 
If there is a single camera connected, just pass 0.

The multicast ip corresponds to the address the client is listening to.

The debug keyword enables debug mode in which additional debug screens are opened (recommended). Simply type "debug" to enable.

**How to adapt the lighting**

1. Make sure the exposure time is as low as possible<br />
2. Adjust the Saturation low and high value so that only the light bulbs are visibble<br />
3. Adjust the values assossiated with blob detection:<br />

- Convexity: How perfect is the blob (e.g. a chip is missing)<br />
- BlobColor: Color of the blob (Best left default value)<br />
- Circularity: Roundness of the blob<br />
- Inertia: Deformation of the blob<br />
- Area: How many pixels in size<br />
