using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class About : GenericScreen 
{
	public Text NameField, versionField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = null;

		FillUserInfo();
	}

	private void FillUserInfo()
	{
		NameField.text = UserManager.user.name;
		versionField.text = "Versão " + MiscAPI.GetVersion();
	}
}
