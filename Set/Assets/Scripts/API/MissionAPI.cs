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
		WebAPI.apiPlace = "/mission/show/" + missionID + "/";
		return WebAPI.Get();
	}

	public static WWW SendMissionResponse (Mission mission, MissionResponse missionResponse)
	{
		WWWForm responseForm = new WWWForm ();
 		responseForm.AddField("user_id", missionResponse.user_id);
 		responseForm.AddField ("answerable_id", missionResponse.mission_id);
 		responseForm.AddField ("answerable_type", "Mission");

 		if (mission.gps_enabled) 
 			responseForm.AddField ("coordinates", missionResponse.coordinates); 

 		if (mission.text_enabled)
 			responseForm.AddField ("text", missionResponse.text);

		if (mission.photo_enabled)
 			responseForm.AddBinaryData("photo", missionResponse.photo, "Photo.png", "image/png");

 		if (mission.audio_enabled)
 			responseForm.AddBinaryData("audio", missionResponse.audio, "voice.wav", "audio/wav");

		WebAPI.apiPlace = "/answer/create/";

		return WebAPI.Post(responseForm);
	}
}
