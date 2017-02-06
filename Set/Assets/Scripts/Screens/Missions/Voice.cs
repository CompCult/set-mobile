using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Voice : GenericScreen 
{
	public Text microphoneDescription;
	public AudioSource audioSource;
	public GameObject microphoneIcon, stopIcon;
	
	private bool isRecording;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		AudioRec.audioSource = audioSource;
		isRecording = false;

		if (MissionManager.mission.photo_enabled)
			backScene = "Media";
		else
			backScene = "Description";
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

	public void ContinueMission()
	{
		Mission mission = MissionManager.mission;

		if (isRecording || audioSource.clip == null)
		{
			AlertsAPI.instance.makeToast("Termine sua gravação antes de continuar", 1);
			return;
		}
		else 
		{
			var filepath = Path.Combine(Application.persistentDataPath, "voice.wav");
			MissionManager.missionResponse.audio = System.IO.File.ReadAllBytes(filepath);
		}

		if (mission.gps_enabled)
			LoadScene("GPS");
		else if (mission.text_enabled)
			LoadScene("Write");
		else 
			LoadScene("Send");
	}
}
