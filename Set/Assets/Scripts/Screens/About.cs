using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class About : GenericScreen 
{
	public Text versionField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		versionField.text = "Versão " + MiscAPI.GetVersion();
	}
}
