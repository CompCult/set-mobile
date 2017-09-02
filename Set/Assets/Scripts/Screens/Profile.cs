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
	public InputField nameField, emailField, cpfField, registryField, phoneField;
	public Dropdown courseField, institutionField;

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
		emailField.text = user.email;
		cpfField.text = user.cpf;
		registryField.text = user.registry;
		phoneField.text = user.phone;
		
		for (int i=0; i < courseField.options.Count; i++)
			if (courseField.options[i].text.Equals(user.course))
				courseField.value = i;

		for (int i=0; i < institutionField.options.Count; i++)
			if (institutionField.options[i].text.Equals(user.institution))
				institutionField.value = i;

		courseField.RefreshShownValue();
		institutionField.RefreshShownValue();
	}

	public void UpdateUserInfo()
	{
		int id = UserManager.user.id;
		
		string name = nameField.text,
		email = emailField.text,
		cpf = cpfField.text,
		registry = registryField.text,
		course = courseField.captionText.text,
		institution = institutionField.captionText.text,
		phone = phoneField.text;

		if (!CheckFields(name, email, cpf, registry, phone))
			return;

		WWW updateRequest = UserAPI.UpdateUser(id, name, email, cpf, registry, phone, course, institution);
		ProcessUpdate(updateRequest);
	}

	private void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == "") 
		{
			Debug.Log("Update response: " + Response);
			
			AlertsAPI.instance.makeToast("Perfil atualizado", 1);
			UpdateLocalUser();
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

		user.name = nameField.text;
		user.email = emailField.text;
		user.cpf = cpfField.text;
		user.registry = registryField.text;
		user.phone = phoneField.text;
		user.course = courseField.captionText.text;
		user.institution = institutionField.captionText.text;

		UserManager.UpdateUser(user);
	}

	private bool CheckFields (string name,string email, string cpf, string registry, string phone)
	{
		string errorMessage = "";

		if (name.Length < 3)
			errorMessage = "Seu nome deve conter pelo menos 3 caracteres.";
		if (!CheckEmail(email))
			errorMessage = "Insira um e-mail válido.";
		if (cpf.Length < 14)
			errorMessage = "Insira um CPF válido.\nO formato correto é 11122233344.";
		if (registry.Length < 9)
			errorMessage = "Insira uma identificação válida.\nPor exemplo, uma matrícula tem formato 111222999.";
		if (phone.Length < 14)
			errorMessage = "Insira um número de telefone válido.\nInsira seu telefone com DDD.";

		if (errorMessage != "") 
		{
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
}
