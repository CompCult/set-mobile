using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestAPI
{
	public static WWW RequestPublicMisions ()
	{
		WebAPI.apiPlace = "/mission/public/";
		WebAPI.pvtKey = "ec689306c5";

		return WebAPI.Get();
	}

	public static WWW RequestPrivateMission (string missionID)
	{
		WebAPI.apiPlace = "/mission/" + missionID + "/";
		WebAPI.pvtKey = "ec689306c5";

		return WebAPI.Get();
	}

	public static WWW SendMissionResponse (Mission mission, MissionResponse missionResponse)
	{
		WWWForm responseForm = new WWWForm ();
 		responseForm.AddField("user_id", missionResponse.user_id);
 		responseForm.AddField ("mission_id", missionResponse.activity_id);

 		if (mission.gps_enabled) 
 			responseForm.AddField ("coordinates", missionResponse.coord); 

 		if (mission.text_enabled)
 			responseForm.AddField ("text", missionResponse.text);

		if (mission.photo_file)
 			responseForm.AddBinaryData("photo", missionResponse.photo, "Photo.png", "image/png");

 		if (mission.audio_file)
 			responseForm.AddBinaryData("audio", missionResponse.audio, "voice.wav", "audio/wav");

		WebAPI.apiPlace = "/answer/";
		WebAPI.pvtKey = "ec689306c5";

		return WebAPI.Post(responseForm);
	}
}
