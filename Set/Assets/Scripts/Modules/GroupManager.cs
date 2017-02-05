using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager
{
	public static Group group;

	public static void UpdateGroup (string json)
	{
		JsonUtility.FromJsonOverwrite(json, group);
	}

	public static void UpdateGroup (Group newGroup)
	{
		group = newGroup;
	}
}