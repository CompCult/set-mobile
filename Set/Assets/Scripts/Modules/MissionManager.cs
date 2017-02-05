using UnityEngine;
using System.Collections;

public static class MissionManager
{
	public static Mission mission;
	public static MissionResponse missionResponse;

	public static void UpdateMission (string json)
	{
		JsonUtility.FromJsonOverwrite(json, mission);
		missionResponse = new MissionResponse ();
	}

	public static void UpdateMission (Mission newMission)
	{
		mission = newMission;
		missionResponse = new MissionResponse ();
	}
}
