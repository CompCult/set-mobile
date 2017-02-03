using UnityEngine;
using System.Collections;

public class Activity {

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

	public Activity CreateActivity (string json)
	{
		return JsonUtility.FromJson<Activity>(json);
	}

	public override string ToString()
	{
		return ("Nome:" + name + "/" +
			"Desc:" + description + "/" +
			"Texto:" + text + "/" +
			"Tipo:" + type + "/" +
			"Local:" + location);
	}
}
