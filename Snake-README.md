## Snake:
Collected Observations:
- 9 raycasts that return distance to the object hit
- 18 raycasts that return the transform.position.x or transform.position.y value (9 raycasts each)
- agent's X and Y position

Discrete Actions:
- Four actions to move the agent up, down, left and right

Reward System:
- Positive reward (.75f) for eating an apple
- Small negative reward (-.00001f) for being idle 
- Negative reward (-1f) for colliding into wall or body part (Done() is called here)

Training Hyperparameters:
- default hyperparameters, though this will be something I experiment with in the future.
    
Gifs:
