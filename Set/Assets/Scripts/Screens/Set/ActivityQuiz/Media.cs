using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Media : GenericScreen 
{
	public Text title;
	public GameObject cameraField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		CameraDevice.cameraPlane = cameraField;
		backScene = "Activity Home";

		CameraDevice.ShowCameraImage();
		UpdateActivityTexts();
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			LoadBackScene();

			CameraDevice.StopCameraImage();
		}
	}
	
	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
	}

	public void RecordPhoto()
	{
		CameraDevice.RecordPhoto();
		ProgressActivity();

		Debug.Log("Foto capturada");
	}

	public void ProgressActivity()
	{
		Activity activity = QuestManager.activity;

		if (CameraDevice.Photo != null)
			QuestManager.activityResponse.photo = CameraDevice.Photo.EncodeToPNG();

		if (activity.audio_file)
			LoadScene("Voice");
		else if (activity.gps_enabled)
			LoadScene("GPS");
		else if (activity.text_enabled)
			LoadScene("Write");
		else 
			LoadScene("Send");
	}
}
