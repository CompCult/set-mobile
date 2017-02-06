using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissionResponse 
{
	public int mission_id,
	user_id,
	group_id;

	public string text,
	coordinates;

	public byte[] photo,
	audio;
}
