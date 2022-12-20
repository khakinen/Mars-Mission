
# mars-mission : Rover in the Mars

Problem:

Nasa has recently created a new department to build software/middleware for remote devices in space. Nasa has developed a Mars Rover that runs on the platform you selected for this task. This rover must navigate around a plateau of the planet Mars. Nasa headquarters on Earth will send a command to the rover in Mars indicating the dimensions of the plateau and a movement sequence the rover must follow. If more than one rover is deployed, then the Nasa command will contain more than one rover command.  

**Movement String**  
M: move to next grid location  
L: turn left  
R: turn right  
 
**Example Nasa Command**

5 5  
1 2 N  
LML  
 
The “5 5” part of the string that indicates the size of the plateau.  “1 2 N” indicates that the rover is positioned on grid square 1,2 and is pointing north.  So, if the rover moved, it would move in the direction it was facing, in this case north.  The last part of the command is the movement sequence.  “LML” means, ‘turn left, move, turn left’.

 

Additionally, the rover must not drive off the plateau. Should a human error occur, e.g. should an operator try to drive the multi-million-dollar rover off the plateau, the rover should stop and await rescue.

There can also be two or more rovers, in which case the instructions for all rovers will be sent.  Of course, the size of the exploration plateau will be the same.  For example, the command below will instruct two rovers on the 5 by 5 plateau:

 

5 5  
1 2 N  
LML  
3 3 E  
MMR

------------------


Solution:

* To run the project:  (in any terminal or command prompt of a computer which has dotnet SDK installed)

  * mars-mission/RoverInTheMars> **dotnet run** [ENTER]

* To run the tests:

  * mars-mission/RoverInTheMars.Test> **dotnet test** [ENTER]

* To change the input command, edit this file :

  * mars-mission/RoverInTheMars/input.txt

Remarks:

* In case of move commands are being sent to rovers which require moving to the same grid square concurrently, rovers are waiting each other to avoid colliding. 
  * For instance; Rover1 got a instruction to move to Square(2,3)
  * If there is any other rover in the Square(2,3), Rover1 waits for them to leave
  * Rover1 will get into the Square(2,3) at the moment they left (max in 1 sec)

* If a rover has completed its mission or fell in await-rescue case, it just waits there which means that, that grid square will be blocked forever. This case could be seen in the console logs.

* Funny story about the mission of Mars:
  * Missions are being started with missionstarter.
  * Factories create relevant objects. 
  * RoverDrivers drive the rovers. 
  * RoverDrivers ask help TrafficPolice  to avoid colliding when they need to move. 
  * So traffic police is responsible for keeping the rovers alive. 
  * All rovers move and turn concurrently right after the mission starts.
  * Logger always gives spoilers for the every action that happens in the Mars
