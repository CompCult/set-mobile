﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Register : GenericScreen 
{
	public InputField nameField, emailField, registryField, 
	institutionField, courseField, cpfField, phoneField, 
	passField, repPassField;

	public void Start () 
	{
		AlertsAPI.instance.Init();

		backScene = "Login";
	}
	
	public void SignUp () 
	{
		string name = nameField.text,
		email = emailField.text,
		registry = registryField.text,
		institution = institutionField.text,
		course = courseField.text,
		cpf = cpfField.text,
		phone = phoneField.text,
		pass = passField.text,
		repPass = repPassField.text;

		if (!AreFieldsCorrect(name, email, registry, institution, course, cpf, phone, pass, repPass))
			return;

		AlertsAPI.instance.makeToast("Completando registro...", 1);

		WWW registerRequest = LoginAPI.RequestRegister(name, email, cpf, registry, phone, institution, course, pass);
		ProcessRegister (registerRequest);
	}

	public void ProcessRegister (WWW registerRequest)
	{
		string Response = registerRequest.text,
		Error = registerRequest.error;

		if (Error == null) 
		{
			Debug.Log("Response Register: " + Response);

			AlertsAPI.instance.makeToast("Registrado(a) com sucesso.", 1);
			LoadScene("Login");
		}
		else 
		{
			Debug.Log("Response Error: " + Error);

			AlertsAPI.instance.makeAlert("E-mail em uso!\nInsira outro endereço de e-mail para continuar.", "OK");
		}
	}

	public bool AreFieldsCorrect (string name, string email, string registry, 
								  string institution, string course, string cpf, 
								  string phone, string password, string repPassword)
	{
		if (name.Length < 3) 
		{
			AlertsAPI.instance.makeAlert("Nome muito curto!\nSeu nome deve conter pelo menos 3 caracteres.", "OK");
			return false;
		}

		if (!CheckEmail(email)) 
		{
			AlertsAPI.instance.makeAlert("E-mail inválido!\nInsira seu e-mail corretamente e tente novamente.", "OK");
			return false;
		}

		if (password.Length < 6 || repPassword.Length < 6 || password != repPassword)
		{
			AlertsAPI.instance.makeAlert("As senhas devem conter pelo menos 3 caracteres e serem iguais.", "OK");
			return false;
		}

		if (registry.Length < 7)
		{
			AlertsAPI.instance.makeAlert("Número de matrícula ou registro inválido.", "OK");
			return false;
		}

		if (institution.Length < 3)
		{
			AlertsAPI.instance.makeAlert("Nome da universidade muito curto. Insira a sigla ou nome completo corretamente.", "OK");
			return false;
		}

		if (course.Length < 2)
		{
			AlertsAPI.instance.makeAlert("Insira o nome do curso corretamente. Lembre-se de digitá-lo por completo.", "OK");
			return false;
		}

		if (cpf.Length < 14)
		{
			AlertsAPI.instance.makeAlert("Insira um CPF válido.", "OK");
			return false;
		}

		if (phone.Length < 13)
		{
			AlertsAPI.instance.makeAlert("Insira um número de telefone válido.", "OK");
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
