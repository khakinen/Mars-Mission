# mars-mission : RoverInTheMars


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
