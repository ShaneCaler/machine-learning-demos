using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RayPerceptionV2 : MonoBehaviour
{
    protected List<float> perceptionBuffer = new List<float>();
	protected List<float> testPerceptionBuffer = new List<float>();

	public virtual List<float> Perceive(float rayDistance,
		float[] rayAngles, string[] detectableObjects,
		float startOffset, float endOffset)
	{
		return perceptionBuffer;
	}


	public virtual List<float> TestPerceive(float rayDistance,
		float[] rayAngles, string[] detectableObjects,
		float startOffset, float endOffset, bool findX, bool findY)
	{
		return testPerceptionBuffer;
	}

	/// <summary>
	/// Converts degrees to radians.
	/// </summary>
	public static float DegreeToRadian(float degree)
	{
		return degree * Mathf.PI / 180f;
	}

}
