using UnityEngine;
using System.Collections;

public static class UserManager
{
	public static User user;

	public static void UpdateUser(User newUser)
	{
		user = newUser;
	}

	public static void UpdateUser(string json)
	{
		JsonUtility.FromJsonOverwrite(json, user);
	}
}
