using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using SingleShadePlugin;

public static class GPSManager
{
	private static double[] _location;
	public static double[] location { get { return _location; } }

	public static void StartGPS()
	{
		if (Application.platform != RuntimePlatform.Android) 
			AlertsAPI.instance.makeToast("Dispositivo sem serviço de localização", 1);
		else
			InputLocation.Start();
	}

	public static void StopGPS()
	{
		if (Application.platform != RuntimePlatform.Android) 
			AlertsAPI.instance.makeToast("Dispositivo sem serviço de localização", 1);
		else
			if (IsActive())
				InputLocation.Stop();
	}

	public static bool IsActive()
	{
		return (InputLocation.isEnabledByUser);
	}

	public static bool ReceivePlayerLocation()
	{
		if (Application.platform != RuntimePlatform.Android) 
		{
			AlertsAPI.instance.makeToast("Dispositivo sem serviço de localização", 1);
			return false;
		}

		_location = new double[2];

		_location[0] = System.Convert.ToDouble(InputLocation.lastData.latitude);
		_location[1] = System.Convert.ToDouble(InputLocation.lastData.longitude);

		return true;
	}

}
