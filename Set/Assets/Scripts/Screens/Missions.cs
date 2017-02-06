using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Missions : GenericScreen 
{
	public GameObject missionCard, publicMissionsList, paperMission;
	public Text missionName, missionDescription;

	public List<Mission> missionList;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		ShowMissionList(true);
		ReceivePublicMissions();
	}

	private void ReceivePublicMissions()
	{
		WWW missionsRequest = MissionAPI.RequestPublicMisions();

		string Response = missionsRequest.text,
		Error = missionsRequest.error;

		if (Error == null)
		{
			FillmissionList(Response);
			CreateActivitiesCards();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber as missões. Tente novamente em alguns instantes.", "Tudo bem");
			LoadBackScene();
		}
	}

	private void FillmissionList(string activities)
    {
		string[] missionsJSON = activities.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	missionList = new List<Mission>();

		foreach (string missionJSON in missionsJSON)
        {
			Mission mission = JsonUtility.FromJson<Mission>(missionJSON);
        	missionList.Add(mission);
        }
    }

    private void CreateActivitiesCards ()
     {
     	Vector3 Position = missionCard.transform.position;

     	if (missionList.Count < 1)
     		return;

     	foreach (Mission mission in missionList)
        {
        	missionName.text = mission.name;
            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(missionCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);

            Debug.Log(mission.name);
        }

        Destroy(missionCard);
     }

     public void SelectActivity(Text missionName)
     {
     	foreach (Mission mission in missionList)
     	{
     		if (mission.name == missionName.text)
     		{
     			MissionManager.UpdateMission(mission);

     			missionDescription.text = MissionManager.mission.description;
     			ShowMissionList(false);

     			break;
     		}
     	}
     }

     public void ShowMissionList(bool value)
     {
     	publicMissionsList.SetActive(value);
		paperMission.SetActive(!value);
     }
}
