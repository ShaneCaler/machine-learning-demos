# Unity Machine Learning Agents Demonstrations
To test the capabilities of Unity's ML-Agents framework, I've created several mini-games where agents are taught to play using reinforcement learning.

# Scenes:
## Flappy Bird:
Collected Observations:
- 7 raycasts that return distance to the object hit
- player's Y position
- X and Y distance from player to nearest goal object (an invisible trigger object placed between columns)

Discrete Actions:
- One action for adding jump force to player's rigidbody

Reward System:
- Small positive reward (.1f) for staying alive
- Positive reward (.5f) for passing through goal objects 
- Negative reward (-1f) for collided into floor, ceiling or a column