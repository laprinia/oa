# oa 🌌🤖
#### *Collaborative Unity 3D space expedition game*
#### *Inspired by the Skyward Sword [timeshift mechanic](https://www.youtube.com/watch?v=rDg-M9IKC34&ab_channel=PerfectlyNintendo)*

![Picture1](https://user-images.githubusercontent.com/51471463/165779195-235cdc99-a40e-470d-b6cd-bdd324394fa6.png)

### 📷Key Mechanics:

 **⛵Navigating**:

https://user-images.githubusercontent.com/51471463/165772789-ca85da2d-9ac9-4b0a-82ab-1c13dc0d99b3.mp4
 
 * In the game, our main character repairs a robot that you can control for a limited time.

 * Inspired by the mechanic from Skyward Sword, within a radius, the robot views the world from the past and can collect resources.
 
 * The whole mechanic is done through a shader, that takes two separate textures for the environment (one being the beat-down version of the other), the radius of the spawned robot and the color of the circle.
 
 * All environment GameObjects, such as the ground or rocks, use this shader. Some of them have scripts that make GameObjects appear using animations.

![image](https://user-images.githubusercontent.com/51471463/165775032-30713f7c-4fc2-49cb-bc07-4487d329026e.png)

 **🔨Building**:

https://user-images.githubusercontent.com/51471463/165779988-b8263608-60e2-4216-b74a-d3d242e3c694.mp4

* The main objective of the game is to collect all resources, which stack up in the main player's inventory.
* Having all the resources, the player can activate the building platform, which animates, adding Materials one by one.
* When all materials are activated, the Timeshift logic is reversed.


![image](https://user-images.githubusercontent.com/51471463/165779792-ee4ba7fc-4b92-4067-bce4-0daf4f657d17.png)

 **🍃Misc Additions**:

 * Cinemachine switcher that switches between Cinemachine cameras smoothly, to transition between main characters.
 * NavMesh Agents that appear in the Timeshift radius and follow, chase, attack the main character.
 * Blend Trees for animators.
 * Movement.
 * Inventory system.

  ![Picture2](https://user-images.githubusercontent.com/51471463/165781737-f24cc351-1169-4f27-bfdb-fe10685cb944.png)
