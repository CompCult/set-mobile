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

		WebAPI.apiPlace = "/group/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		Debug.Log("Group Name:" + groupName);
		Debug.Log("Owner ID: " + ownerID);
		Debug.Log("URL: " + WebAPI.url + WebAPI.apiPlace + WebAPI.pvtKey);

		return WebAPI.Post(createForm);
	}

	public static WWW DeleteGroup (int groupID)
	{
		WWWForm deleteForm = new WWWForm ();
		deleteForm.AddField ("group_id", groupID);

		WebAPI.apiPlace = "/group/remove/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(deleteForm);
	}

	public static WWW AddGroupMember(string memberEmail, int groupID)
	{
		WWWForm addForm = new WWWForm ();
		addForm.AddField ("user_email", memberEmail);
		addForm.AddField ("group_id", groupID);

		WebAPI.apiPlace = "/group/add-user/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(addForm);
	}

	public static WWW RemoveGroupMember(string memberEmail, int groupID)
	{
		WWWForm removeForm = new WWWForm ();
		removeForm.AddField ("user_email", memberEmail);
		removeForm.AddField ("group_id", groupID);

		WebAPI.apiPlace = "/group/remove-user/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(removeForm);
	}

	public static WWW RequestGroups()
	{
		WebAPI.apiPlace = "/user/" + UserManager.user.id + "/groups/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Get();
	}

	public static WWW RequestGroupInfo()
	{
		WebAPI.apiPlace = "/group/" + GroupManager.group.id + "/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Get();
	}
}
