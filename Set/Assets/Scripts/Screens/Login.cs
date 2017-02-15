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
		if (PlayerPrefs.HasKey("ChangeTrees-Email"))
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
		string error = loginRequest.error,
		response = loginRequest.text;

		if (error == null) 
		{
			Debug.Log("Response received: " + response);

			if (response.Contains(LocalizationManager.GetText("PasswordIncorrect")))
			{
				AlertsAPI.instance.makeAlert("Senha incorreta!\nVerifique se inseriu os dados corretamente.", "OK");
				return;
			}

			if (response.Contains(LocalizationManager.GetText("EmailDontExist")))
			{
				AlertsAPI.instance.makeAlert("E-mail não econtrado!\nVerifique se inseriu os dados corretamente.", "OK");
				return;
			}		

			if (rememberMe.isOn)
				PlayerPrefs.SetString("ChangeTrees-Email", emailField.text);
			else
				PlayerPrefs.DeleteKey("ChangeTrees-Email");

			UserManager.UpdateUser(response);

			if (!UserManager.user.active)
			{
				AlertsAPI.instance.makeAlert("Seu registro ainda não foi validado. Aguarde até um tutor validá-lo.", "Entendi");
				return;
			}

			LoadScene("Home");
		}
		else 
		{
			Debug.Log("Error received: " + error);

			if (error.Contains("404"))
				AlertsAPI.instance.makeAlert("Servidor não encontrado!\nContate o administrador do sistema.", "OK");
			else 
				if (error.Contains("500"))
				{
					AlertsAPI.instance.makeAlert("Ops, falha no servidor!\nContate o administrador do sistema.", "OK");
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
