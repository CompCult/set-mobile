using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityHome : GenericScreen 
{
	public Text title, text;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Search Activity";

		UpdateActivityTexts();
	}
	
	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
		text.text = QuestManager.activity.text;
	}

	public void ProgressActivity()
	{
		Activity activity = QuestManager.activity;

		// Sequence: Photo > Voice > Gps > Text > Send
		if (activity.photo_file)
			LoadScene("Media");
		else if (activity.audio_file)
			LoadScene("Voice");
		else if (activity.gps_enabled)
			LoadScene("GPS");
		else if (activity.text_enabled)
			LoadScene("Write");
	}
}
