using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScreen {

	public Text NameField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Login";

		UpdateFields();
	}

	public void UpdateUser ()
	{
		Debug.Log("Updating user with ID " + UsrManager.user.id);

		WWW userRequest = Authenticator.RequestUser(UsrManager.user.id);
		
		string Response = userRequest.text,
		Error = userRequest.error;

		if (Error == null)
		{
			Debug.Log("Response: " + Response);

			UnityAndroidExtras.instance.makeToast("Informações do usuário atualizadas", 1);
			UsrManager.UpdateUser(userRequest.text);

			UpdateFields();
		}
		else 
		{
			Debug.Log("Error: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao atualizar usuário. Tente novamente.", 1);
		}
	}

	public void UpdateFields()
	{
		NameField.text = UsrManager.user.name;
	}
}
