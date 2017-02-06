using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Groups : GenericScreen 
{
	public Text nameField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		FillUserInfo();
	}

	private void FillUserInfo()
	{
		nameField.text = UserManager.user.name;
	}
}
