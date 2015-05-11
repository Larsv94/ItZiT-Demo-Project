# ItZiT-Demo-Project
A demo project in Unity3D for a internship at ItZiT Cross Media Creators

This is a simple demo to proof to ItZiT I'm also competend in Unity3D and C#.

The original demo asked for by ItZiT stated that atleast the following mechanics had to be implemented.
  - A camera that can move through the level via user input
  - 1 sound attached to the ground. The closer the camera is to the ground, the harder the sound is.
  - 1 sound attached to a 3D object in the world. The closer the camera is to the object, the harder the sound is.
  - 1 sound in a room as if the player is inside a building. The sound is only audible inside the room
  
As bonus:
  - A horn sound that is emitted as soon as the player hits the "T" key
  
The above took me around 5 hours including the install of Unity and finding out how the general structure works opposed to Unreal Engine 4


As this project was incredibly easy for me I decided to add some extra's
The following mechanics where added by myself on own initiative:

  - A keyboard Event Manager
      -Register Events on Key Press
      -Register Events on Key Release
      -Register Events regardless of Key Release or Key Press
      -Unregister Events
      -Remove all events on a Key
  - A 3D Sound from Unity itself
      -This isn't that special on it's own but shows I'm capable to use predefined tools to accomplish tasks in a easy and quick way
  -Predefined Camera movement based on Targets
      -Adds the ability to add gameobjects to the camera control and a key for each object. The camera moves to the object when the key is pressed and negates user input during transition
  - A disco room
      -Door opens on player proximity
      -Lights change color randomly when player is inside room
      -Music starts playing when player is inside room
  - A toggle button for the sound the ground emits
      -This is a easy solution so the player can listen to other sounds without always hearing what the ground has to say

The extra's I've added took me around 3-4 hours
