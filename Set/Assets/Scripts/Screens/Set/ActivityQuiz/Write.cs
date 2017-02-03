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
		UnityAndroidExtras.instance.Init();

		if (QuestManager.activity.gps_enabled)
			backScene = "GPS";
		else if (QuestManager.activity.audio_file)
			backScene = "Voice";
		else if (QuestManager.activity.photo_file)
			backScene = "Media";
		else 
			backScene = "Activity Home";
	}

	public bool CheckTexts() 
	{
		if (userText.text.Length < 5)
			return false;

		return true;
	}

	public void ProgressActivity()
	{
		if (CheckTexts())
		{
			QuestManager.activityResponse.text = userText.text;
			LoadScene("Send");
		}
		else 
			UnityAndroidExtras.instance.makeToast("Escreva o texto da missão antes de continuar", 1);
	}
}
