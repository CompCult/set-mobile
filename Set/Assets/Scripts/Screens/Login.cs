using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Login : GenericScreen 
{
	public InputField emailField, passField;
	public Toggle rememberMe;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = null;

		CheckSavedEmail();
	}

	private void CheckSavedEmail()
	{
		// If user saved an email, enables Remember button
		if (PlayerPrefs.HasKey("Email"))
		{
			emailField.text = PlayerPrefs.GetString("ChangeTrees-Email");
			rememberMe.isOn = true;
		}
		else 
		{
			rememberMe.isOn = false;
		}
	}
	
	public void SignIn () 
	{
		string email = emailField.text,
		password = passField.text;

		if (!AreFieldsCorrect(email, password))
			return;

		AlertsAPI.instance.makeToast("Conectando-se...", 1);

		WWW loginRequest = LoginAPI.RequestUser(email, password);
		ProcessLogin (loginRequest);
	}

	private void ProcessLogin (WWW loginRequest)
	{
		string Error = loginRequest.error,
		Response = loginRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response received: " + Response);

			if (rememberMe.isOn)
				PlayerPrefs.SetString("ChangeTrees-Email", emailField.text);
			else
				PlayerPrefs.DeleteKey("ChangeTrees-Email");

			UserManager.UpdateUser(Response);
			LoadScene("Home");
		}
		else 
		{
			Debug.Log("Error received: " + Error);

			if (Error.Contains("404"))
				AlertsAPI.instance.makeAlert("Dados incorretos!\nVerifique se inseriu seu e-mail corretamente.", "OK");
			else if (Error.Contains("500"))
			{
				if (Application.platform == RuntimePlatform.Android) 
					AlertsAPI.instance.makeAlert("Dados incorretos!\nVerifique seu e-mail e senha.", "OK");
			}
			else 
				AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao conectar-se com o servidor. Tente novamente mais tarde.", "OK");
		}
	}

	private bool AreFieldsCorrect (string email, string password)
	{
		if (!CheckEmail(email)) 
		{
			AlertsAPI.instance.makeAlert("E-mail inválido!\nInsira seu e-mail corretamente e tente mais uma vez.", "OK");
			return false;
		}

		if (password.Length < 6) 
		{
			AlertsAPI.instance.makeAlert("Senha curta!\nSua senha deve conter pelo menos 6 caracteres", "OK");
			return false;
		}

		return true;
	}

	private bool CheckEmail(string email)
	{
		string emailRegularExpression = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

	 	Regex reg = new Regex(emailRegularExpression);
		return reg.IsMatch(email);
	}
}
