using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager
{
	public static Group group;

	public static void UpdateGroup (string json)
	{
		if (group != null)
			JsonUtility.FromJsonOverwrite(json, group);
		else
			group = JsonUtility.FromJson<Group>(json);
	}

	public static void UpdateGroup (Group newGroup)
	{
		group = newGroup;
	}
}