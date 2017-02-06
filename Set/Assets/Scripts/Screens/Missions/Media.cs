using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Media : GenericScreen 
{
	public GameObject cameraField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		CameraDevice.cameraPlane = cameraField;
		backScene = "Description";

		CameraDevice.ShowCameraImage();
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			LoadBackScene();
		}
	}

	public void RecordPhoto()
	{
		CameraDevice.RecordPhoto();
		ContinueMission();

		Debug.Log("Foto capturada");
	}

	public void ContinueMission()
	{
		Mission mission = MissionManager.mission;

		if (CameraDevice.Photo != null)
			MissionManager.missionResponse.photo = CameraDevice.Photo.EncodeToPNG();

		if (mission.audio_enabled)
			LoadScene("Voice");
		else if (mission.gps_enabled)
			LoadScene("GPS");
		else if (mission.text_enabled)
			LoadScene("Write");
		else 
			LoadScene("Send");
	}
}
