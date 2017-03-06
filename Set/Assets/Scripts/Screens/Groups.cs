using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Groups : GenericScreen 
{
	public InputField newGroupField;
	public GameObject groupCard;
	public Text groupName;
	
	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		RequestUserGroups();
	}

	private void RequestUserGroups ()
	{
		WWW groupsRequest = GroupAPI.RequestGroups();

		string Response = groupsRequest.text,
		Error = groupsRequest.error;

		if (Error == null)
		{
			Debug.Log("Response:" + Response);
			
			GroupManager.UpdateGroups(Response);
			CreateGroupsCard();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber seus grupos. Tente novamente em alguns instantes.", "Tudo bem");
			LoadBackScene();
		}
	}

    private void CreateGroupsCard ()
     {
     	Vector3 Position = groupCard.transform.position;
     	foreach (Group group in GroupManager.groups)
        {
        	groupName.text = group.name;
            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(groupCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        }

        Destroy(groupCard);
    }

     public void SelectGroup (Text groupName)
     {
     	foreach (Group group in GroupManager.groups)
     	{
     		if (group.name == groupName.text)
     		{
     			GroupManager.UpdateGroup(group);
     			LoadScene("GroupScreen");
     			break;
     		}
     	}
     }

	public void CreateGroup()
	{
		string newGroupName = newGroupField.text;
		int ownerID = UserManager.user.id;

		if (newGroupName == null || newGroupName == "")
			return;

		AlertsAPI.instance.makeToast("Criando o grupo " + newGroupName, 1);

		WWW createRequest = GroupAPI.CreateGroup(newGroupName, ownerID);
		ProcessCreation (createRequest);
	}

	private void ProcessCreation(WWW createRequest)
	{
		string Error = createRequest.error,
		Response = createRequest.text;

		if (Error == null) 
		{
			AlertsAPI.instance.makeToast("Grupo criado com sucesso", 1);
			ReloadScene();

			Debug.Log("Group creation response: " + Response);
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve uma falha ao criar seu grupo. Tente novamente em alguns instantes.", "Tudo bem");
			Debug.Log("Group creation error: " + Error);
		}
	}
}
