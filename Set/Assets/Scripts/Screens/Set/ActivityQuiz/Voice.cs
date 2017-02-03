using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Voice : GenericScreen 
{
	public Text title, microphoneDescription;
	public AudioSource audioSource;
	public GameObject microphoneIcon, stopIcon;
	
	private bool isRecording;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		AudioRec.audioSource = audioSource;
		isRecording = false;

		if (QuestManager.activity.photo_file)
			backScene = "Media";
		else
			backScene = "Activity Home";

		UpdateActivityTexts();
	}
	
	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
	}

	public void RecordMicrophone()
	{
		isRecording = !isRecording;
		AudioRec.RecordAudio();

		if (isRecording)
		{
			microphoneIcon.SetActive(false);
			stopIcon.SetActive(true);

			microphoneDescription.text = "Parar de gravar";
		}
		else
		{
			microphoneIcon.SetActive(true);
			stopIcon.SetActive(false);

			microphoneDescription.text = "Gravar sua voz";
		}
	}

	public void ListenAudio()
	{
		AudioRec.ListenAudio ();
	}

	public void ProgressActivity()
	{
		Activity activity = QuestManager.activity;

		if (isRecording || audioSource.clip == null)
		{
			UnityAndroidExtras.instance.makeToast("Termine sua gravação antes de continuar", 1);
			return;
		}
		else 
		{
			var filepath = Path.Combine(Application.persistentDataPath, "voice.wav");
			QuestManager.activityResponse.audio = System.IO.File.ReadAllBytes(filepath);
		}

		if (activity.gps_enabled)
			LoadScene("GPS");
		else if (activity.text_enabled)
			LoadScene("Write");
		else 
			LoadScene("Send");
	}
}
