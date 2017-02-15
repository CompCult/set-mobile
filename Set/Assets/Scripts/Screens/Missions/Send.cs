using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Send : GenericScreen 
{
	public void Start () 
	{
		AlertsAPI.instance.Init();

		if (MissionManager.mission.text_enabled)
			backScene = "Write";
		else if (MissionManager.mission.gps_enabled)
			backScene = "GPS";
		else if (MissionManager.mission.audio_enabled)
			backScene = "Voice";
		else if (MissionManager.mission.photo_enabled)
		 	backScene = "Media";
		else 
			backScene = "Description";
	}

	public void SendMissionResponse ()
	{
		AlertsAPI.instance.makeToast("Enviando...", 1);

		MissionManager.missionResponse.user_id = UserManager.user.id;
		MissionManager.missionResponse.mission_id = MissionManager.mission.id;

		WWW responseForm = MissionAPI.SendMissionResponse(MissionManager.mission, MissionManager.missionResponse);
		ProcessSend(responseForm);
	}

	private void ProcessSend (WWW responseForm)
	{
		string Error = responseForm.error,
		Response = responseForm.text;

		if (Error == null) 
		{
			Debug.Log("Response from send mission: " + Response);

			AlertsAPI.instance.makeToast("Enviado com sucesso", 1);
			LoadScene("Missions");
		}
		else 
		{
			Debug.Log("Error sending: " + Error);

			if (Error.Contains("404 "))
				AlertsAPI.instance.makeAlert("Que pena!\nParece que essa atividade foi removida ou já expirou.", "Tudo bem");
			else 
				AlertsAPI.instance.makeAlert("Ops!\nHouve um problema no servidor. Tente novamente mais tarde.", "Tudo bem");
		}
	}
}
