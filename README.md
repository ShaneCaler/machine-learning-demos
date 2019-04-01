# Unity Machine Learning Agents Demonstrations
To test the capabilities of Unity's ML-Agents framework, I've created several mini-games where agents are taught to play using reinforcement learning, or more specifically, a technique known as Proximal Policy Optimization, implemented in Google's Tensorflow library.

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

Training Hyperparameters:
    max_steps: 2000000
    batch_size: 512
    buffer_size: 4096
    learning_rate: 2e-4
    
Notes:


### Dependencies:
- Unity ML-Agents 0.7.0
- Python 3.6.4
- <a href="https://bayat.itch.io/platform-game-assets">Free Platform Game Assets</a>
