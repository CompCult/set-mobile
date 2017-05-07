using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Media : GenericScreen 
{
	public GameObject cameraField;
	public GameObject[] rotateObjects;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		CameraDevice.photo2DPlane = cameraField;
		backScene = "Description";

		CameraDevice.StartCameraDevice();
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

	public void RotateImage()
	{
		CameraDevice.RotateImage();

		foreach(GameObject obj in rotateObjects)
		{
			obj.transform.Rotate(0,0,90);
		}
	}

	public void SwitchCamera()
	{
		CameraDevice.SwitchCamera();
	}

	public void ContinueMission()
	{
		Mission mission = MissionManager.mission;

		if (CameraDevice.photoCaptured != null)
			MissionManager.missionResponse.photo = CameraDevice.photoCaptured.EncodeToPNG();

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
