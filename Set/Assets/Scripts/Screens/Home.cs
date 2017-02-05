using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScreen {

	public Text NameField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = null;

		FillUserInfo();
	}

	private void FillUserInfo()
	{
		NameField.text = UserManager.user.name;
	}
}
