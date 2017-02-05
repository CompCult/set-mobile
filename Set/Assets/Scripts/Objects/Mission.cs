using UnityEngine;
using System.Collections;

[System.Serializable]
public class Mission 
{
	public int id;

	public string name,
				  description,
				  text,
				  type,
				  location;

	public bool gps_enabled,
				text_enabled,
				video_file,
			    photo_file,
			    audio_file;
}
