using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GroupAPI
{
	public static WWW CreateGroup(string groupName, int ownerID)
	{
		WWWForm createForm = new WWWForm ();
		createForm.AddField ("name", groupName);
		createForm.AddField ("owner_id", ownerID);

		WebAPI.apiPlace = "/group/create/";

		Debug.Log("Group Name:" + groupName);
		Debug.Log("Owner ID: " + ownerID);
		Debug.Log("URL: " + WebAPI.url + WebAPI.apiPlace + WebAPI.pvtKey);

		return WebAPI.Post(createForm);
	}

	public static WWW DeleteGroup (int groupID)
	{
		WWWForm deleteForm = new WWWForm ();
		deleteForm.AddField ("id", groupID);

		WebAPI.apiPlace = "/group/destroy/";

		return WebAPI.Post(deleteForm);
	}

	public static WWW AddGroupMember(string memberEmail, int groupID)
	{
		WWWForm addForm = new WWWForm ();
		addForm.AddField ("user_email", memberEmail);
		addForm.AddField ("group_id", groupID);

		WebAPI.apiPlace = "/group/add-member/";

		return WebAPI.Post(addForm);
	}

	public static WWW RemoveGroupMember(string memberEmail, int groupID)
	{
		WWWForm removeForm = new WWWForm ();
		removeForm.AddField ("user_email", memberEmail);
		removeForm.AddField ("group_id", groupID);

		WebAPI.apiPlace = "/group/remove-member/";

		return WebAPI.Post(removeForm);
	}

	public static WWW RequestGroups()
	{
		WebAPI.apiPlace = "/user/" + UserManager.user.id + "/show-groups/";
		return WebAPI.Get();
	}

	public static WWW RequestGroupInfo()
	{
		WebAPI.apiPlace = "/group/show/" + GroupManager.group.id + "/";
		return WebAPI.Get();
	}

	public static WWW SendGroupMessage (string message, User author)
	{
		WWWForm emailForm = new WWWForm ();
		emailForm.AddField ("subject", "[Change Trees] Mensagem do Grupo " + GroupManager.group.name);
		emailForm.AddField ("body", message);
		emailForm.AddField ("author", author.email);

		Debug.Log("Author: " + author.email + " / Message: " + message);

		WebAPI.apiPlace = "/group/" + GroupManager.group.id + "/notify/";
		return WebAPI.Post(emailForm);
	}
}
