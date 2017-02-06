using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : GenericScreen 
{
	public GameObject loadingIcon, alertMenu;

	public void Start () 
	{
		LocalizationManager.Start();
		backScene = null;

		StartCoroutine(SplashTime());
	}

	private IEnumerator SplashTime () 
	{
		if (PlayerPrefs.HasKey("ChangeTrees-StartAlert"))
		{
			loadingIcon.SetActive(true);
			alertMenu.SetActive(false);
		}
		else
		{
			loadingIcon.SetActive(false);
			alertMenu.SetActive(true);
		}

		yield return new WaitForSeconds(2);

		// Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		if (loadingIcon.activeSelf)
		{
			LoadScene("Login");
		}
	}

	public void ContinueToLogin(bool DefinitiveContinue)
	{
		if (DefinitiveContinue)
			PlayerPrefs.SetString("ChangeTrees-StartAlert", "NoAlert");

		LoadScene("Login");
	}
}
