using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissionResponse 
{
	public int activity_id,
	user_id;

	public string text,
	coord;

	public byte[] photo,
	audio;
}
