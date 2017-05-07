using UnityEngine;
using System.Collections;

public static class CameraDevice
{
	public static GameObject photo2DPlane;
	public static Texture2D photoCaptured;

	private static WebCamTexture currentCamera;
	private static WebCamDevice[] camerasAvaible;

	public static void StartCameraDevice()
	{
		camerasAvaible = GetCameras();

		if (camerasAvaible.Length > 0)
		{
			currentCamera = new WebCamTexture();
			ShowCameraImage();
		}
	}

	public static void RecordPhoto()
	{
		if (GetCameras().Length < 1)
			return;

		new WaitForEndOfFrame(); 

		photoCaptured = new Texture2D(currentCamera.width, currentCamera.height);
		photoCaptured.SetPixels(currentCamera.GetPixels());
		photoCaptured.Apply();

		photo2DPlane.GetComponent<Renderer>().material.mainTexture = photoCaptured;
	}

	public static void SwitchCamera()
	{
		if (GetCameras().Length < 2)
		{
			AlertsAPI.instance.makeAlert("Desculpe, não encontramos outra câmera disponível.", "Entendi");
			return;
		}

		for (int i=0; i < GetCameras().Length; i++)
		{
			if (!camerasAvaible[i].name.Equals(currentCamera.deviceName))
			{
				currentCamera.Stop();
				
				currentCamera.deviceName = camerasAvaible[i].name;
				ShowCameraImage();
				
				break;
			}
		}
	}

	public static void RotateImage()
	{
		photo2DPlane.transform.Rotate(0, 0, 90);
	}

	private static void ShowCameraImage()
	{
		photo2DPlane.GetComponent<Renderer>().material.mainTexture = currentCamera;
		currentCamera.Play();
	}

	private static void StopCameraImage()
	{
		photo2DPlane.GetComponent<Renderer>().material.mainTexture = null;

		if (currentCamera != null)
			currentCamera.Stop();
	}

	private static WebCamDevice[] GetCameras()
	{
		return WebCamTexture.devices;
	}
}
