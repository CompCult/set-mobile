using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionAPI
{
	public static WWW RequestPublicMisions ()
	{
		WebAPI.apiPlace = "/mission/public/";
		return WebAPI.Get();
	}

	public static WWW RequestPrivateMission (string missionID)
	{
		WebAPI.apiPlace = "/mission/private/" + missionID + "/";
		return WebAPI.Get();
	}

	public static WWW SendMissionResponse (Mission mission, MissionResponse missionResponse)
	{
		WWWForm responseForm = new WWWForm ();

		if (missionResponse.user_id != null)
			responseForm.AddField("user_id", missionResponse.user_id);
		if (missionResponse.group_id != null)
			responseForm.AddField("group_id", missionResponse.group_id);

 		responseForm.AddField ("answerable_id", missionResponse.mission_id);
 		responseForm.AddField ("answerable_type", missionResponse.type);

 		Debug.Log("ID: " + missionResponse.mission_id + " / Type: " + missionResponse.type);

 		if (mission.gps_enabled) 
 		{
 			Debug.Log("Coordinates: " + missionResponse.coordinates);
 			responseForm.AddField ("coordinates", missionResponse.coordinates); 
 		}

 		if (mission.text_enabled)
 		{
 			Debug.Log("Text: '" + missionResponse.text + "'");
 			responseForm.AddField ("text", missionResponse.text);
 		}

		if (mission.photo_enabled)
		{
			Debug.Log("Photo Ready");
 			responseForm.AddBinaryData("photo", missionResponse.photo, "Photo.png", "image/png");
		}

 		if (mission.audio_enabled)
 		{
 			Debug.Log("Audio Ready");
 			responseForm.AddBinaryData("audio", missionResponse.audio, "voice.wav", "audio/wav");
 		}

		WebAPI.apiPlace = "/answer/create/";

		return WebAPI.Post(responseForm);
	}
}
