using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Write : GenericScreen 
{
	public InputField userText;

	public void Start () 
	{
		AlertsAPI.instance.Init();

		if (MissionManager.mission.gps_enabled)
			backScene = "GPS";
		else if (MissionManager.mission.audio_enabled)
			backScene = "Voice";
		else if (MissionManager.mission.photo_enabled)
			backScene = "Media";
		else 
			backScene = "Description";
	}

	private bool CheckTexts() 
	{
		if (userText.text.Length < 5)
			return false;

		return true;
	}

	public void ContinueMission()
	{
		if (CheckTexts())
		{
			MissionManager.missionResponse.text = userText.text;
			LoadScene("Send");
		}
		else 
			AlertsAPI.instance.makeToast("Escreva o texto da missão antes de continuar", 1);
	}
}
