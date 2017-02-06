using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Description : GenericScreen 
{
	public Text missionTitle, missionDescription;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Missions";

		FillMissionFieldsInfo();
	}
	
	public void FillMissionFieldsInfo () 
	{	
		missionTitle.text = MissionManager.mission.name;
		missionDescription.text = MissionManager.mission.text;
	}

	public void ContinueMission()
	{
		Mission mission = MissionManager.mission;

		// Sequence: Photo > Voice > Gps > Text > Send
		if (mission.photo_file)
			LoadScene("Media");
		else if (mission.audio_file)
			LoadScene("Voice");
		else if (mission.gps_enabled)
			LoadScene("GPS");
		else if (mission.text_enabled)
			LoadScene("Write");
	}
}
