﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroupScreen : GenericScreen 
{
	public GameObject memberCard, memberList, emailField;
	public Button deleteMemberButton, deleteGroupButton, exitGroupButton, emailButton, answersButton, addMemberButton;
	public Text groupName, memberName, memberEmail, newMemberEmail;

	private bool isOwner, isWritingEmail = false;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Groups";

		groupName.text = GroupManager.group.name;

		if (GroupManager.group.owner_id == UserManager.user.id)
			emailButton.GetComponent<Button>().interactable = true;
		else 
			emailButton.GetComponent<Button>().interactable = false;

		RequestGroupInfo();
	}

	public override void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (isWritingEmail)
				ReloadScene();
			else 
				LoadBackScene();
		}
	}

	private void RequestGroupInfo ()
	{
		WWW infoRequest = GroupAPI.RequestGroupInfo();

		string Response = infoRequest.text,
		Error = infoRequest.error;

		if (Error == null)
		{
			GroupManager.UpdateGroup(Response);

			// If the user is the group owner
			if (GroupManager.group.owner_id == UserManager.user.id)
			{
				isOwner = true;
				deleteGroupButton.gameObject.SetActive(true);
				exitGroupButton.gameObject.SetActive(false);
			}
			else
			{
				isOwner = false;
				exitGroupButton.gameObject.SetActive(true);
				deleteGroupButton.gameObject.SetActive(false);
			}

			CreateMembersCard();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber as informações do grupo. Poderia tentar novamente em alguns instantes?", "Tudo bem");
			LoadBackScene();
		}
	}

    private void CreateMembersCard ()
     {
     	Vector3 Position = memberCard.transform.position;
     	foreach (User member in GroupManager.group.members)
        {
        	memberName.text = member.name;
        	memberEmail.text = member.email;

        	if (member.id == GroupManager.group.owner_id) // Owners cant delete themselves
        		deleteMemberButton.gameObject.SetActive(false);
        	else 
        		if (isOwner) // Owners can delete other members
        			deleteMemberButton.gameObject.SetActive(true);

            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(memberCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        }

        Destroy(memberCard);
    }

    public void SendEmail()
    {
    	if (isWritingEmail)
    	{
    		User author = UserManager.user;    		
    		string message = emailField.GetComponent<InputField>().text;
    	
    		WWW emailForm = GroupAPI.SendGroupEmail(message, author);

    		string error = emailForm.error,
   			response = emailForm.text;

   			if (error != null || response.Contains("success"))
   			{
 				Debug.Log("Email response:" + response);

   				AlertsAPI.instance.makeAlert("Mensagem enviada com sucesso!", "OK");
   				ReloadScene();
   			}
   			else 
   			{
   				Debug.Log("Email error:" + error);
   				AlertsAPI.instance.makeAlert("Falha ao enviar mensagem. Verifique sua conexão com a internet e tente novamente em instantes.", "OK");
   			}
    	}
    	else 
    	{
    		memberList.SetActive(false);
    		emailField.SetActive(true);

 			// TODO - Loop to deactive all
 			answersButton.interactable = false;
 			exitGroupButton.interactable = false;
 			deleteGroupButton.interactable = false;
 			addMemberButton.interactable = false;
    	}

    	isWritingEmail = !isWritingEmail;
    }

    public void AddMember()
	{
		string memberEmail = newMemberEmail.text;
		int groupID = GroupManager.group.id;

		if (memberEmail == null || memberEmail == "")
			return;

		foreach (User member in GroupManager.group.members)
        {
        	if (memberEmail == member.email)
        	{
        		AlertsAPI.instance.makeAlert("Essa pessoa já faz parte do grupo", "OK");
        		return;
        	}
        }

		AlertsAPI.instance.makeToast("Adicionando " + memberEmail, 1);

		WWW addRequest = GroupAPI.AddGroupMember(memberEmail, groupID);
		ProcessAdding (addRequest);
	}

	private void ProcessAdding(WWW addRequest)
	{
		string Error = addRequest.error,
		Response = addRequest.text;

		if (Error == null) 
		{
			AlertsAPI.instance.makeToast("Membro adicionado", 1);
			ReloadScene();
		}
		else 
		{
			if (Error.Contains("404 "))
				AlertsAPI.instance.makeAlert("E-mail não encontrado!\nParece que não há um jogador com esse e-mail. Você digitou corretamente?", "Corrigir");
			else 
				AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao adicionar o membro. Tente novamente em alguns instantes.", "Tudo bem");
			
			Debug.Log("Member add error: " + Error);
		}
	}

	public void LogoutGroup()
	{
		Text myEmail = gameObject.AddComponent<Text>();
     	myEmail.text = UserManager.user.email;

		RemoveMember(myEmail);
	}

	public void RemoveMember(Text removeEmail)
	{
		WWW removeRequest;

		foreach (User member in GroupManager.group.members)
        {
        	if (removeEmail.text == member.email)
        	{
        		int groupID = GroupManager.group.id;
        		string memberEmail = member.email;

        		Debug.Log("Removing e-mail: " + memberEmail);

        		removeRequest = GroupAPI.RemoveGroupMember(memberEmail, groupID);
        		ProcessRemove(removeRequest);
        		break;
        	}
        }
	}

	private void ProcessRemove(WWW addRequest)
	{
		string Error = addRequest.error,
		Response = addRequest.text;

		if (Error == null) 
		{
			if (isOwner)
			{
				AlertsAPI.instance.makeToast("Membro removido", 1);

				Scene scene = SceneManager.GetActiveScene();
	            SceneManager.LoadScene(scene.name);
	        }
	        else 
	        {
	        	AlertsAPI.instance.makeToast("Você saiu do grupo", 1);
	        	LoadBackScene();
	        }
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve uma falha no processo de retirada. Tente novamente mais tarde.", "Tudo bem");
			Debug.Log("Member remove error: " + Error);
		}
	}

	public void DeleteGroup()
	{
		int groupID = GroupManager.group.id;

		AlertsAPI.instance.makeToast("Excluindo grupo...", 1);

		WWW removeRequest = GroupAPI.DeleteGroup(groupID);
		ProcessDelete (removeRequest);
	}

	private void ProcessDelete(WWW removeRequest)
	{
		string Error = removeRequest.error,
		Response = removeRequest.text;

		if (Error == null) 
		{
			AlertsAPI.instance.makeToast("Grupo excluído", 1);
			LoadBackScene();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao excluir o grupo. Tente novamente em instantes.", "Tudo bem");
			Debug.Log("Group delete error: " + Error);
		}
	}

}
