using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour 
{
  
	public float horizontalSpeed,
	verticalSpeed,
	finalPosition,
 	amplitude;

 	private Vector3 tempPosition;

 	public void Start () 
  {
  		tempPosition = transform.position;
 	}

 	public void FixedUpdate () 
  	{
  		tempPosition.x += horizontalSpeed;
		tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude + 2.35f;
  		transform.position = tempPosition;

  		if (tempPosition.x >= finalPosition)
  			tempPosition.x = -finalPosition;
  	}
}

