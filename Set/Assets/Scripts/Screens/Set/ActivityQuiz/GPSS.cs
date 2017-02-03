using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPSS : GenericScreen 
{
	public Text title;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		if (QuestManager.activity.audio_file)
			backScene = "Voice";
		else if (QuestManager.activity.photo_file)
			backScene = "Media";
		else 
			backScene = "Activity Home";

		GPS.StartGPS();
		UpdateActivityTexts();
	}

	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
	}

	public void RequestCoordinates(string step)
	{
		bool requestSuccess = GPS.ReceivePlayerLocation ();
		
		if (!requestSuccess || GPS.location == null)
			return;

		if (GPS.location[0] == 0 || GPS.location[1] == 0 || !GPS.IsActive())
		{
			UnityAndroidExtras.instance.makeToast("Ative o serviço de localização do celular", 1);
			return;
		}

		UnityAndroidExtras.instance.makeToast("Localização obtida", 1);
		string playerLocation = GPS.location[0] + " | " + GPS.location[1];

		switch (step) 
		{
			case "coord_start":
				QuestManager.activityResponse.coord_start = playerLocation; break;
			case "coord_mid":
				QuestManager.activityResponse.coord_mid = playerLocation; break;
			case "coord_end":
				QuestManager.activityResponse.coord_end = playerLocation; break;
		}
	}

	public void ProgressActivity()
	{
		if (QuestManager.AreCoordsFilled ())
		{ 
			GPS.StopGPS();

			if (QuestManager.activity.text_enabled)
				LoadScene("Write");
			else
				LoadScene("Send");
		} 
		else
		{
			UnityAndroidExtras.instance.makeToast("Marque o local da missão", 1);
		}
	}
}
