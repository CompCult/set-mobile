using UnityEngine;
using System.Collections;

[System.Serializable]
public class Photo
{
	public int id, notification_id, 
	answer_id, hq_id;
	
	public string name;

	public override string ToString()
	{
		return "hq_id: " + hq_id; 
	}
}
