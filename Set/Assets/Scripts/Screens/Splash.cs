using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : GenericScreen 
{
	public GameObject fruitIcon, loadingIcon, alertMenu;

	public void Start () 
	{
		LocalizationManager.Start();
		backScene = null;

		StartCoroutine(SplashTime());
	}

	public void ContinueToLogin(bool DefinitiveContinue)
	{
		if (DefinitiveContinue)
			PlayerPrefs.SetString("ChangeTrees-StartAlert", "NoAlert");

		LoadScene("Login");
	}

	private IEnumerator SplashTime () 
	{
		// Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		if (IsUpdated())
		{
			if (PlayerPrefs.HasKey("ChangeTrees-StartAlert"))
			{
				yield return new WaitForSeconds(2);
				LoadScene("Login");
			}
			else
			{
				loadingIcon.SetActive(false);
				alertMenu.SetActive(true);
			}
		}
		else
		{
			loadingIcon.SetActive(false);
			alertMenu.SetActive(false);
			fruitIcon.SetActive(true);
		}
	}

	private bool IsUpdated()
	{
		WWW versionRequest = MiscAPI.RequestVersion();

		string Error = versionRequest.error,
		Response = versionRequest.text;

		if (Error == null)
		{
			if (MiscAPI.GetVersion() == Response)
			{
				Debug.Log("Version Updated");
				return true;
			}
			else
			{
				AlertsAPI.instance.makeAlert("Versão incorreta!\nAcesse nossa página de aplicativo na Play Store e atualize seu Change Trees para a última versão.", "Entendi");
				return false;
			}
		}
		else 
		{
			Debug.Log("Error on version: " + Response);
			AlertsAPI.instance.makeAlert("Falha ao obter sua versão!\nVerifique sua conexão e tente novamente em breve.", "OK");
			return false;
		}
	}

	public void OpenPlayStore()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.compcult.changetrees");
	}
}
