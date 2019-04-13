# Unity Machine Learning Agents Demonstrations
To test the capabilities of Unity's ML-Agents framework, I've created several mini-games where agents are taught to play using reinforcement learning, or more specifically, a technique known as Proximal Policy Optimization, implemented in Google's Tensorflow library.

# Scenes:
## Flappy Bird:
Collected Observations:
- 9 raycasts that return distance to the object hit
- agent's Y position
- X and Y distance from player to nearest goal object (an invisible trigger object placed between columns)

Discrete Actions:
- One action for adding jump force to player's rigidbody

Reward System:
- Small positive reward (.05f) for staying alive
- Positive reward (.5f) for passing through goal objects 
- Negative reward (-1f) for colliding into floor, ceiling or a column

Training Hyperparameters:
- beta: 1.0e-2
- hidden_units: 256
- time_horizon: 32
- learning_rate: 5.0e-3
- batch_size: 512
- buffer_size: 20480
- normalize: false
    
Gifs:
- <a href="https://imgur.com/a/liMf9WD">500,000 steps into training</a> - the agent has started to learn how to pass through columns and has made it through a max of 10 in one attempt
- <a href="https://imgur.com/a/YR5dmFh">1,000,000 steps into training</a> - the agent has now made it through 29 columns in one attempt. Training is slow and the agent still fails early-on a lot of the time, but it does appear to be learning.
- <a href="https://imgur.com/a/pdhPZ33">2,000,000 steps into training</a> - the agent has now made it through a max of 34 columns in one attempt, it seems that it hit a "wall" of sorts and stopped learning as quickly. I suspect my hyperparameters may be off and need tweaking, but at least the agent is getting good scores overall.

### Dependencies:
- Unity ML-Agents 0.7.0
- Python 3.6.4
- <a href="https://bayat.itch.io/platform-game-assets">Free Platform Game Assets</a>
