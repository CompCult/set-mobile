using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RankingAPI 
{
	public static List<User> userRanking;
	public static List<Group> groupRanking;

	public static WWW RequestGroupRanking()
	{
		WebAPI.apiPlace = "/group/rank/";
		return WebAPI.Get();
	}

	public static WWW RequestUserRanking ()
	{
		WebAPI.apiPlace = "/user/rank/";

		return WebAPI.Get();
	}

	public static void UpdateUserRanking(string json)
	{
		string[] usersJSON = json.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
	    userRanking = new List<User>();

		foreach (string userJSON in usersJSON)
	    {
	    	Debug.Log("User: " + userJSON);

			User user = JsonUtility.FromJson<User>(userJSON);
	       	userRanking.Add(user);
	    }
	}

	public static void UpdateGroupRanking(string json)
	{
		string[] groupsJSON = json.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Replace(":}",":null}").Split ('%');
	    groupRanking = new List<Group>();

		foreach (string groupJSON in groupsJSON)
	    {
	    	Debug.Log("Group: " + groupJSON);

			Group group = JsonUtility.FromJson<Group>(groupJSON);
	       	groupRanking.Add(group);
	    }
	}
}