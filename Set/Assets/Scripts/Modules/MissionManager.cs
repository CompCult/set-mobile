using UnityEngine;
using System.Collections;

public static class MissionManager
{
	public static Mission mission;
	public static MissionResponse missionResponse;

	public static void UpdateMission (string json)
	{
		if (mission != null)
			JsonUtility.FromJsonOverwrite(json, mission);
		else
			mission = JsonUtility.FromJson<Mission>(json);

		missionResponse = new MissionResponse ();
	}

	public static void UpdateMission (Mission newMission)
	{
		mission = newMission;
		missionResponse = new MissionResponse ();
	}
}
