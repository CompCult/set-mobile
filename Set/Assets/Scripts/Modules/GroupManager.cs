using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager
{
	public static Group group;
	public static List<Group> groups;

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

	public static void UpdateGroups (string json)
	{
		string[] groupsJSON = json.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
	    groups = new List<Group>();

		foreach (string groupJSON in groupsJSON)
	    {
			Group group = JsonUtility.FromJson<Group>(groupJSON);
	       	groups.Add(group);
	    }
	}

	public static void UpdateGroups (List<Group> newGroups)
	{
		groups = newGroups;
	}

	public static Group GetGroup(string groupName)
	{
		foreach (Group group in groups)
		{
			if (group.name.Equals(groupName))
				return group;
		}

		return null;
	}
}