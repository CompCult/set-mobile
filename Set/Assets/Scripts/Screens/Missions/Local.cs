using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Local : GenericScreen 
{
	public void Start () 
	{
		AlertsAPI.instance.Init();

		if (MissionManager.mission.audio_enabled)
			backScene = "Voice";
		else if (MissionManager.mission.photo_enabled)
			backScene = "Media";
		else 
			backScene = "Description";

		GPSManager.StartGPS();
	}

	public void RequestCoordinates()
	{
		bool requestSuccess = GPSManager.ReceivePlayerLocation ();
		
		if (!requestSuccess || GPSManager.location == null)
			return;

		if (GPSManager.location[0] == 0 || GPSManager.location[1] == 0 || !GPSManager.IsActive())
		{
			AlertsAPI.instance.makeAlert("GPS desligado!\nAtive o serviço de localização do celular na barra superior do dispositivo.", "Entendi");
			return;
		}

		AlertsAPI.instance.makeToast("Localização obtida", 1);
		string playerLocation = GPSManager.location[0] + " | " + GPSManager.location[1];

		MissionManager.missionResponse.coordinates = playerLocation;
	}

	public void ContinueMission()
	{
		if (MissionManager.missionResponse.coordinates != null)
		{ 
			GPSManager.StopGPS();

			if (MissionManager.mission.text_enabled)
				LoadScene("Write");
			else
				LoadScene("Send");
		} 
		else
		{
			AlertsAPI.instance.makeToast("Marque o local da missão", 1);
		}
	}
}
