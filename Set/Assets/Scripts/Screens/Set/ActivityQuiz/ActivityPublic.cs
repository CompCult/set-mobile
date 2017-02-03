using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActivityPublic : GenericScreen 
{
	public GameObject activityCard, continueButton;
	public Text activityName, activityDescription;

	public List<Activity> activitiesList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Search Activity";

		ReceivePublicActivities();
	}

	private void ReceivePublicActivities()
	{
		WWW activitiesRequest = Authenticator.RequestPublicActivities();

		string Response = activitiesRequest.text,
		Error = activitiesRequest.error;

		if (Error == null)
		{
			FillActivitiesList(Response);
			CreateActivitiesCards();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao receber as missões públicas", 1);
			LoadBackScene();
		}
	}

	private void FillActivitiesList(string activities)
    {
		string[] activitiesJSON = activities.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	activitiesList = new List<Activity>();

		foreach (string activityJSON in activitiesJSON)
        {
			Activity activity = QuestManager.CreateActivity(activityJSON);
        	activitiesList.Add(activity);
        }
    }

    private void CreateActivitiesCards ()
     {
     	Vector3 Position = activityCard.transform.position;

     	if (activitiesList.Count < 1)
     		return;

     	foreach (Activity activity in activitiesList)
        {
        	activityName.text = activity.name;
            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(activityCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);

            Debug.Log(activity.name);
        }

        Destroy(activityCard);
     }

     public void SelectActivity(Text activityName)
     {
     	foreach (Activity activity in activitiesList)
     	{
     		if (activity.name == activityName.text)
     		{
     			QuestManager.UpdateActivity(activity);
     			activityDescription.text = activity.description;

     			activityDescription.gameObject.SetActive(true);
     			continueButton.SetActive(true);
     			break;
     		}
     		else 
     		{
     			activityDescription.gameObject.SetActive(false);
     			continueButton.SetActive(false);
     		}
     	}
     }
}
