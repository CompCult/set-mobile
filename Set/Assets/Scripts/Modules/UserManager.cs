using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UserManager
{
	public static User user;

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
}
