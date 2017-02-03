using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class ProfileAddress : GenericScreen {

	public InputField zipField,
	streetField,
	numberField,
	districtField,
	cityField,
	stateField,
	complementField;

	private int noAddress = 0;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		ReceiveAddress ();

		backScene = "Profile";
	}
	
	public void ReceiveAddress () 
	{
		User user = UsrManager.user;

		// If the user have address, receive it.
		if (user.address != noAddress)
		{
			WWW addressRequest = Authenticator.RequestAddress(user.address);
			ProcessAddress(addressRequest);
		}
	}

	public void ProcessAddress (WWW addressRequest)
	{
		string Error = addressRequest.error,
		Response = addressRequest.text;

		if (Error == null) 
		{
			AddressManager.UpdateAddress(Response);
			UpdateFields();
		}
		else 
		{
			Debug.Log("Error on get address: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao obter seu endereço. Tente novamente mais tarde.", 1);
			LoadScene(backScene);
		}
	}

	public void UpdateFields()
	{
		Address address = AddressManager.address;

		zipField.text = address.zipcode;
		streetField.text = address.street;
		numberField.text = address.number;
		districtField.text = address.district;
		cityField.text = address.city;
		stateField.text = address.state;
		complementField.text = address.complement;
	}

	public void UpdateAddress()
	{
		string zipcode = zipField.text,
		street = streetField.text,
		number = numberField.text,
		district = districtField.text,
		city = cityField.text,
		state = stateField.text,
		complement = complementField.text;
		User user = UsrManager.user;

		if (!CheckFields(zipcode, state))
			return;

		// Checks if the user have an address
		if (user.address != noAddress)
		{
			WWW updateRequest = Authenticator.UpdateAddress(zipcode, street, number, district, city, state, complement);
			ProcessUpdate(updateRequest);
		}
		else 
		{
			WWW createAddressRequest = Authenticator.CreateAddress(zipcode, street, number, district, city, state, complement);
			ProcessCreate(createAddressRequest);
		}
	}

	public void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response for update address: " + Response);

			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update address: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao obter seu endereço. Tente novamente mais tarde.", 1);
		}
	}

	public void ProcessCreate(WWW createAddressRequest)
	{
		string Error = createAddressRequest.error,
		Response = createAddressRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response for create address: " + Response);
			UsrManager.SetAddressID(int.Parse(Response));

			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on create address: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao criar seu endereço. Tente novamente mais tarde.", 1);
		}
	}

	public bool CheckFields(string zipcode, string state)
	{
		string errorMessage = "";

		if (zipcode.Length != 8)
			errorMessage = "Insira um CEP válido.";
		if (state.Length != 2)
			errorMessage = "Insira um estado válido.";

		if (errorMessage != "")
		{
			UnityAndroidExtras.instance.makeToast(errorMessage, 1);
			return false;
		}

		return true;
	}
}
