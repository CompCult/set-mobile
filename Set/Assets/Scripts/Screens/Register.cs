using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Register : GenericScreen {

	// Use this for initialization
	[Header("Screen elements")]
	public InputField nameField, emailField, passField, repPassField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		backScene = "Login";
	}
	
	public void SignUp () 
	{
		string name = nameField.text,
		email = emailField.text,
		pass = passField.text,
		repPass = repPassField.text;

		if (!AreFieldsCorrect(name, email, pass, repPass))
			return;

		UnityAndroidExtras.instance.makeToast("Criando seu Áqueo...", 1);

		WWW registerRequest = Authenticator.RequestRegister(name, email, pass);
		ProcessRegister (registerRequest);
	}

	public void ProcessRegister (WWW registerRequest)
	{
		string Error = registerRequest.error;

		if (Error == null) 
		{
			UnityAndroidExtras.instance.makeToast("Agora você pertence ao mundo de Aqua!", 1);
			LoadScene("Login");
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("E-mail já registrado", 1);
		}
	}

	public bool AreFieldsCorrect (string name, string email, string password, string repPassword)
	{
		if (name.Length < 3) 
		{
			UnityAndroidExtras.instance.makeToast("O nome deve conter pelo menos 3 caracteres", 1);
			return false;
		}

		if (password.Length < 6 || repPassword.Length < 6 || password != repPassword)
		{
			UnityAndroidExtras.instance.makeToast("As senhas devem conter pelo menos 6 caracteres e serem iguais", 1);
			return false;
		}

		if (!CheckEmail(email)) 
		{
			UnityAndroidExtras.instance.makeToast("Insira um e-mail válido", 1);
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
}
