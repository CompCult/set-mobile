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
	public Text versionInfo;
	public Toggle rememberMe;
	public Button registerButton, loginButton;

	private int devModeCounter = 0;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = null;
		
		CheckSavedEmail();
		CheckVersion();
	}

	public void CheckSavedEmail()
	{
		// If user saved an email, enables Remember button
		if (PlayerPrefs.HasKey("Email"))
		{
			emailField.text = PlayerPrefs.GetString("Email");
			rememberMe.isOn = true;
		}
		else 
		{
			rememberMe.isOn = false;
		}
	}

	public void CheckVersion()
	{
		WWW versionRequest = Authenticator.CheckVersion();

		string errorText = versionRequest.error,
		versionText = versionRequest.text;

		if (errorText == null)
		{
			if (versionText == versionInfo.text)
			{
				Debug.Log("Updated version! v" + versionText);
				ToggleButtons(true);
			}
			else 
			{
				UnityAndroidExtras.instance.makeToast("Versão desatualizada", 1);
				UnityAndroidExtras.instance.makeToast("Atualize em nossa página na loja de aplicativos", 1);
			}
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Ocorreu um erro ao verificar sua versão", 1);
		}
	}

	public void ToggleButtons (bool newState)
	{
		registerButton.interactable = newState;
		loginButton.interactable = newState;
	}
	
	public void SignIn () 
	{
		string email = emailField.text,
		password = passField.text;

		if (!AreFieldsCorrect(email, password))
			return;

		UnityAndroidExtras.instance.makeToast("Entrando em Aqua", 1);

		WWW loginRequest = Authenticator.RequestUserID(email, password);
		ProcessLogin (loginRequest);
	}

	public void ProcessLogin (WWW loginRequest)
	{
		string Error = loginRequest.error,
		Response = loginRequest.text;

		if (Error == null) 
		{
			Debug.Log("ID received: " + Response);

			if (rememberMe.isOn)
				PlayerPrefs.SetString("Email", emailField.text);
			else
				PlayerPrefs.DeleteKey("Email");

			int userID = int.Parse(Response);
			RequestUser(userID);
		}
		else 
		{
			if (Error.Contains("404"))
				UnityAndroidExtras.instance.makeToast("Verifique seu e-mail e senha", 1);
			else if (Error.Contains("500"))
			{
				if (Application.platform == RuntimePlatform.Android) 
					UnityAndroidExtras.instance.makeToast("Nome de usuário ou senha incorretos", 1);
				else
					UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor! Tente novamente mais tarde", 1);
			}
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao conectar! Tente novamente mais tarde", 1);
		}
	}

	public void RequestUser(int userID)
	{
		Debug.Log("Requesting user with ID " + userID);

		WWW userRequest = Authenticator.RequestUser(userID);
		
		string Response = userRequest.text,
		Error = userRequest.error;

		if (Error == null)
		{
			Debug.Log("Response: " + Response);

			UsrManager.UpdateUser(userRequest.text);
			LoadScene("Home");
		}
		else 
		{
			Debug.Log("Error: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao receber usuário. Tente novamente.", 1);
			LoadScene("Login");
		}
	}

	public bool AreFieldsCorrect (string email, string password)
	{
		if (!CheckEmail(email)) 
		{
			UnityAndroidExtras.instance.makeToast("Insira um e-mail válido", 1);
			return false;
		}

		if (password.Length < 6) 
		{
			UnityAndroidExtras.instance.makeToast("A senha deve conter, pelo menos, 6 caracteres", 1);
			return false;
		}

		return true;
	}

	public bool CheckEmail(string email)
	{
		string emailRegularExpression = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

	 	Regex reg = new Regex(emailRegularExpression);
		return reg.IsMatch(email);
	}	

	public void IncrementDevModeCounter()
	{
		devModeCounter++;

		if (devModeCounter == 10)
		{
			devModeCounter = 0;
			WebFunctions.ToggleURL();
		}
	}
}
