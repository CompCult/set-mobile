using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Profile : GenericScreen 
{
	public Text nameField;
	public InputField nameProfileField,
	emailField,
	cpfField,
	registryField,
	courseField,
	instituitionField,
	phoneField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		FillFieldsWithPlayerInfo();

		backScene = "Home";
	}
	
	public void FillFieldsWithPlayerInfo () 
	{
		User user = UserManager.user;

		nameField.text = user.name;
		nameProfileField.text = user.name;
		emailField.text = user.email;
		cpfField.text = user.cpf;
		registryField.text = user.registry;
		phoneField.text = user.phone;
		courseField.text = user.course;
		instituitionField.text = user.instituition;
	}

	public void UpdateUserInfo()
	{
		int id = UserManager.user.id;
		
		string name = nameProfileField.text,
		email = emailField.text,
		cpf = cpfField.text,
		registry = registryField.text,
		course = courseField.text,
		instituition = instituitionField.text,
		phone = phoneField.text;

		if (!CheckFields(name, email, cpf, registry, course, instituition, phone))
			return;

		WWW updateRequest = UserAPI.UpdateUser(id, name, email, cpf, registry, phone, course, instituition);
		ProcessUpdate(updateRequest);
	}

	private void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Update response: " + Response);
			
			AlertsAPI.instance.makeToast("Perfil atualizado", 1);

			UpdateLocalUser();
			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update: " + Error);
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao atualizar seu perfil. Tente novamente em alguns instantes.", "Tudo bem");
		}
	}

	private void UpdateLocalUser()
	{
		User user = UserManager.user;

		user.name = nameProfileField.text;
		user.email = emailField.text;
		user.cpf = cpfField.text;
		user.registry = registryField.text;
		user.phone = phoneField.text;
		user.course = courseField.text;
		user.instituition = instituitionField.text;

		UserManager.UpdateUser(user);
	}

	private bool CheckFields (string name,string email, string cpf, string registry, string course, string instituition, string phone)
	{
		string errorMessage = "";

		if (name.Length < 3)
			errorMessage = "Seu nome deve conter pelo menos 3 caracteres.";
		if (!CheckEmail(email))
			errorMessage = "Insira um e-mail válido.";
		if (cpf.Length < 14 || !CheckCPF(cpf))
			errorMessage = "Insira um CPF válido.\nO formato correto é 111.222.333-44.";
		if (registry.Length < 9)
			errorMessage = "Insira uma identificação válida.\nPor exemplo, uma matrícula tem formato 111222999.";
		if (phone.Length < 11)
			errorMessage = "Insira um número de telefone válido.\nInsira seu telefone com DDD.";

		if (errorMessage != "") {
			AlertsAPI.instance.makeAlert(errorMessage, "OK");
			return false;
		}
		
		return true;
	}

	private bool CheckEmail(string email)
	{
		string strRegex = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(email);
	}	

	private bool CheckCPF(string cpf)
	{
		string strRegex = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(cpf);
	}
}
