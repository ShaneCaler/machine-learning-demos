using System.Collections.Generic;
using UnityEngine;

namespace MLAgents
{

    /// <summary>
    /// Ray perception component. Attach this to agents to enable "local perception"
    /// via the use of ray casts directed outward from the agent. 
    /// </summary>
    public class BirdRayPerception : MonoBehaviour
    {
        List<float> perceptionBuffer = new List<float>();
		List<float> perceptionBuffer2 = new List<float>();
		Vector3 endPosition;
        RaycastHit hit;

        /// <summary>
        /// Creates perception vector to be used as part of an observation of an agent.
        /// </summary>
        /// <returns>The partial vector observation corresponding to the set of rays</returns>
        /// <param name="rayDistance">Radius of rays</param>
        /// <param name="rayAngles">Anlges of rays (starting from (1,0) on unit circle).</param>
        /// <param name="detectableObjects">List of tags which correspond to object types agent can see</param>
        /// <param name="startOffset">Starting heigh offset of ray from center of agent.</param>
        /// <param name="endOffset">Ending height offset of ray from center of agent.</param>
        public List<float> Perceive(float rayDistance, Vector2 startOffset, Vector2 dirOffset, string[] detectableObjects)
        {
            perceptionBuffer.Clear();
            // For each ray sublist stores categorial information on detected object
            // along with object distance.
            if (Application.isEditor)
            {
                Debug.DrawRay(startOffset,
					dirOffset, Color.black, 0.5f, true);
            }

            float[] subList = new float[detectableObjects.Length + 2];
			RaycastHit2D hit = Physics2D.Raycast(startOffset, dirOffset, rayDistance);
			if (hit.collider != null)
			{
				for (int i = 0; i < detectableObjects.Length; i++)
				{
					if (hit.collider.gameObject.CompareTag(detectableObjects[i]))
					{
						//Debug.Log("Adding " + hit.collider.gameObject.name + " to sublist");
						subList[i] = 1;
						subList[detectableObjects.Length + 1] = hit.distance / rayDistance;
						break;
					}
				}
			}
			else
			{
				subList[detectableObjects.Length] = 1f;
			}

			perceptionBuffer.AddRange(subList);

            return perceptionBuffer;
        }
	}
}
