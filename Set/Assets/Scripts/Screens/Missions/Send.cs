using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Send : GenericScreen 
{
	public Dropdown groupsDropDown;

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

		RequestUserGroups();
	}

	private void RequestUserGroups ()
	{
		WWW groupsRequest = GroupAPI.RequestGroups();
		string response = groupsRequest.text,
		error = groupsRequest.error;

		if (error == null)
		{
			GroupManager.UpdateGroups(response);
			FillGroupsDropwdown();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber seus grupos. Verifique sua conexão com a internet.", "Tudo bem");
			LoadBackScene();
		}
	}

	private void FillGroupsDropwdown()
	{
		foreach (Group group in GroupManager.groups) 
        	groupsDropDown.options.Add (new Dropdown.OptionData() {text = group.name});

     	groupsDropDown.RefreshShownValue();
	}

	public void SendMissionResponse ()
	{
		AlertsAPI.instance.makeToast("Enviando...", 1);
		int participants = groupsDropDown.value;

		if (participants == 0)
			MissionManager.missionResponse.user_id = UserManager.user.id;
		else
			MissionManager.missionResponse.group_id = GroupManager.groups[participants-1].id;

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

			if (MissionManager.missionResponse.group_id != null)
			{
				AlertsAPI.instance.makeAlert("Enviado com sucesso!\nAcompanhe o status do envio na página de seu grupo.", "Entendi");
				LoadScene("Home");
			}
			else 
			{
				AlertsAPI.instance.makeAlert("Enviado com sucesso!\nAcompanhe o status do envio na página de envios de resposta.", "Entendi");
				LoadScene("Answers");
			}
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
