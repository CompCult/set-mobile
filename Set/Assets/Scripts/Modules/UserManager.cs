using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UserManager
{
	public static User user;
	public static List<User> ranking;

	public static void UpdateUser(User newUser)
	{
		user = newUser;
	}

	public static void UpdateUser(string json)
	{
		if (user != null)
			JsonUtility.FromJsonOverwrite(json, user);
		else
			user = JsonUtility.FromJson<User>(json);
	}

	public static void UpdateRanking(string json)
	{
		string[] usersJSON = json.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
	    ranking = new List<User>();

		foreach (string userJSON in usersJSON)
	    {
			User user = JsonUtility.FromJson<User>(userJSON);
	       	ranking.Add(user);
	    }
	}
}
