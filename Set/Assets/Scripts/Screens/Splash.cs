using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : GenericScreen 
{
	public void Start () 
	{
		backScene = null;

		CheckAlertPreferences();
	}

	private void CheckAlertPreferences()
	{
		if (PlayerPrefs.HasKey("ChangeTrees-StartAlert"))
		{
			LoadScene("Login");
		}
	}

	public void ContinueToLogin(bool DefinitiveContinue)
	{
		if (DefinitiveContinue)
			PlayerPrefs.SetString("ChangeTrees-StartAlert", "No");

		LoadScene("Login");
	}
}
