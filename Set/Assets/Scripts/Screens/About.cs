using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class About : GenericScreen 
{
	public Text nameField, xpField, versionField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		FillUserInfo();
	}

	private void FillUserInfo()
	{
		nameField.text = UserManager.user.name;
		xpField.text = "EXP " + UserManager.user.xp;
		versionField.text = "Versão " + MiscAPI.GetVersion();
	}
}
