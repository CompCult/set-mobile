using UnityEngine;
using System.Collections.Generic;

public static class WebAPI
{
	private static string urlDev = "https://set-web-dev.herokuapp.com/api",
	urlDefault = "https://set-web.herokuapp.com//api";

	private static string _url = urlDev,
	_pvtKey = "de7206872c37dcab45f6b4ceae174cedb4f5f5e7",
	_apiPlace = "/";

	public static string url { get { return _url; } }
	public static string pvtKey { get { return _pvtKey; } set { _pvtKey = value; } }
	public static string apiPlace { get { return _apiPlace; } set { _apiPlace = value; } }
		
	#pragma warning disable 0219
	public static WWW Get()
	{
		string apiLink = url + apiPlace + pvtKey;
		WWW www = new WWW (apiLink);

		Debug.Log("WebAPI - Get url: " + apiLink);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);

		return www; 
	}

	#pragma warning disable 0219
	public static WWW Post(WWWForm form)
	{
		string apiLink = url + apiPlace + pvtKey;
		WWW www = new WWW(apiLink, form);

		Debug.Log("WebAPI - Post url: " + apiLink);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);

		return www; 
	}

	public static bool IsDev()
	{
		return _url == urlDev;
	}

	public static void ToggleURL()
	{
		if (_url == urlDefault)
		{
			AlertsAPI.instance.makeToast("Modo desenvolvedor ativado", 1);
			_url = urlDev;
		}
		else 
		{
			AlertsAPI.instance.makeToast("Modo desenvolvedor desativado", 1);
			_url = urlDefault;
		}
	}
}