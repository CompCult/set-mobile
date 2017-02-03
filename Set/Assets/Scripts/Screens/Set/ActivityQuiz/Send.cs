using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Send : GenericScreen 
{
	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		if (QuestManager.activity.text_enabled)
			backScene = "Write";
		else if (QuestManager.activity.gps_enabled)
			backScene = "GPS";
		else if (QuestManager.activity.audio_file)
			backScene = "Voice";
		else if (QuestManager.activity.photo_file)
		 	backScene = "Media";
		else 
			backScene = "Activity Home";
	}

	public void SendActivity ()
	{
		UnityAndroidExtras.instance.makeToast("Enviando...", 1);

		QuestManager.activityResponse.user_id = UsrManager.user.id;
		QuestManager.activityResponse.activity_id = QuestManager.activity.id;

		WWW responseForm = Authenticator.SendActivity(QuestManager.activityResponse, QuestManager.activity);
		ProcessSend(responseForm);
	}

	public void ProcessSend (WWW responseForm)
	{
		string Error = responseForm.error,
		Response = responseForm.text;

		if (Error == null) 
		{
			Debug.Log("Response from send activity: " + Response);

			UnityAndroidExtras.instance.makeToast("Enviado com sucesso", 1);
			LoadScene("AquaWorld");
		}
		else 
		{
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("Atividade não encontrada ou já expirada", 1);
			else if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);
		}
	}
}
