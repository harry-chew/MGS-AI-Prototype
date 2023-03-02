# MGS-AI-Prototype

Metal Gear Solid style AI Prototype developed in Unity. This was a 1 day event to create some AI state logic. 

The AI has states: 
Patrol
Chase
Attack

Patrol is the default/idle state and will patrol between any number of given Transforms.

Chase will follow the player if they are seen by the enemy.

Attack will occur if the Enemy manages to catch up with the player.


If out of range after an attack, will return to Chase, if out of range of Chase, will return to previous waypoint.
