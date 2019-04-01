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
- max_steps: 2000000
- batch_size: 512
- buffer_size: 4096
- learning_rate: 2e-4
    
Gifs:
- <a href="https://imgur.com/a/jkmfgsz">500,000 steps into training</a> - the agent has started to learn how to pass through columns and has made it through a max of 4 in one attempt
- <a href="https://imgur.com/a/TLDX14c">1,000,000 steps into training</a> - the agent has now made it through 16 columns in one attempt. Training is slow and the agent still fails early-on a lot of the time, but it does appear to be learning.
- <a href="https://imgur.com/a/8DeO9b6">2,000,000 steps into training</a> - the agent has now made it through a max of 20 columns in one attempt, it seems that it hit a "wall" of sorts and stopped learning as quickly. While it is able to sometimes make it through many columns, I suspect it's more to due to luck of the random column placement. As seen in the gif, it would often jump when the best action would be to fall a little more

### Dependencies:
- Unity ML-Agents 0.7.0
- Python 3.6.4
- <a href="https://bayat.itch.io/platform-game-assets">Free Platform Game Assets</a>
