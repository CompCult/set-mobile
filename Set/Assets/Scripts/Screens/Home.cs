using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScreen 
{
	public Text backNameField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = null;

		backNameField.text = UserManager.user.name;
	}

	public void UpdateUserInfo()
	{
		WWW userRequest = UserAPI.RequestUser(UserManager.user.id);

		string Error = userRequest.error,
		Response = userRequest.text;

		if (Error == "")
		{
			Debug.Log("User updated");

			AlertsAPI.instance.makeToast("Perfil atualizado", 1);
			UserManager.UpdateUser(Response);
		}
		else
		{
			AlertsAPI.instance.makeAlert("Falha ao atualizar!\nVerifique sua conexão e tente novamente.", "OK");
		}
	}
}
